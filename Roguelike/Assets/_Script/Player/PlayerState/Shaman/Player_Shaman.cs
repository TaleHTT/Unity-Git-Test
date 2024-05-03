using System.Collections.Generic;
using UnityEngine;

public class Player_Shaman : PlayerBase
{
    public GameObject treatTarget {  get; set; }
    public List<GameObject> treatDetect {  get; set; }
    public PlayerShamanDeadState shamanDeadState { get; set; }
    public PlayerShamanIdleState shamanIdleState {  get; set; }
    public PlayerShamanAttackState shamanAttackState { get; set; }

    protected override void Awake()
    {
        base.Awake();
        shamanIdleState = new PlayerShamanIdleState(this, stateMachine, "Idle", this);
        shamanDeadState = new PlayerShamanDeadState(this, stateMachine, "Dead", this);
        shamanAttackState = new PlayerShamanAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(shamanIdleState);
    }

    protected override void Update()
    {
        base.Update();
        TreatDetect();
        TreatTarget();
    }
    public void TreatDetect()
    {
        treatDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var player in colliders)
        {
            if (player.GetComponent<PlayerBase>() != null)
                treatDetect.Add(player.gameObject);
        }
    }
    public void TreatTarget()
    {
        float hp = Mathf.Infinity;
        for (int i = 0; i < treatDetect.Count; i++)
        {
            if (hp >= treatDetect[i].GetComponent<PlayerBase>().stats.currentHealth)
            {
                hp = treatDetect[i].GetComponent<PlayerBase>().stats.currentHealth;
                treatTarget = treatDetect[i].gameObject;
            }
        }
    }
}