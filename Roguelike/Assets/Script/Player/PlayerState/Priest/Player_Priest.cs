using System.Collections.Generic;
using UnityEngine;

public class Player_Priest : PlayerBase
{
    public List<GameObject> playerDetects;

    public PlayerPriestIdleState priestIdleState { get; private set; }
    public PlayerPriestDeadState priestDeadState { get; private set; }
    public PlayerPriestAttackState priestAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        priestIdleState = new PlayerPriestIdleState(this, stateMachine, "Idle", this);
        priestDeadState = new PlayerPriestDeadState(this, stateMachine, "Dead", this);
        priestAttackState = new PlayerPriestAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(priestIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(priestDeadState);
        PriestTreatLogci();
    }
    public void PriestTreatLogci()
    {
        float leasthp = Mathf.Infinity;
        for(int i = 0; i < playerDetects.Count; i++)
        {
            if (leasthp > playerDetects[i].GetComponent<CharacterStats>().currentHealth)
            {
                leasthp = playerDetects[i].GetComponent<CharacterStats>().currentHealth;
                treatTarget = playerDetects[i].transform;
            }
        }
    }
}
