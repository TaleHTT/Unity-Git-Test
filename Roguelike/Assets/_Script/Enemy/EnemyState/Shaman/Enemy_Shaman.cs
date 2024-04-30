using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shaman : EnemyBase
{
    public GameObject treatTarget { get; set; }
    public List<GameObject> treatDetect { get; set; }
    public EnemyShamanIdleState shamanIdleState { get; private set; }
    public EnemyShamanDeadState shamanDeadState { get; private set; }
    public EnemyShamanChaseState shamanChaseState { get; private set; }
    public EnemyShamanAttackState shamanAttackState { get; private set; }
    public EnemyShamanPatrolState shamanPatrolState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        shamanDeadState = new EnemyShamanDeadState(this, stateMachine, "Dead", this);
        shamanIdleState = new EnemyShamanIdleState(this, stateMachine, "Idle", this);
        shamanChaseState = new EnemyShamanChaseState(this, stateMachine, "Move", this);
        shamanPatrolState = new EnemyShamanPatrolState(this, stateMachine, "Move", this);
        shamanAttackState = new EnemyShamanAttackState(this, stateMachine, "Attack", this);
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
            if (player.GetComponent<EnemyBase>() != null)
                treatDetect.Add(player.gameObject);
        }
    }
    public void TreatTarget()
    {
        float hp = Mathf.Infinity;
        for (int i = 0; i < treatDetect.Count; i++)
        {
            if (hp >= treatDetect[i].GetComponent<EnemyBase>().stats.currentHealth)
            {
                hp = treatDetect[i].GetComponent<EnemyBase>().stats.currentHealth;
                treatTarget = treatDetect[i].gameObject;
            }
        }
    }
}
