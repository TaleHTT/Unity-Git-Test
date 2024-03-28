using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player_Priest : PlayerBase
{
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
        if (stats.currentHealth <= 0)
            stateMachine.ChangeState(priestDeadState);
    }
}
