using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Archer : PlayerBase
{
    public GameObject arrowPerfab;
    public PlayerArcherIdleState archerIdleState {  get; private set; }
    public PlayerArcherMoveState archerMoveState { get; private set; }
    public PlayerArcherDeadState archerDeadState { get; private set; }
    public PlayerArcherAttackState archerAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        archerIdleState = new PlayerArcherIdleState(this, stateMachine, "Idle", this);
        archerMoveState = new PlayerArcherMoveState(this, stateMachine, "Move", this);
        archerDeadState = new PlayerArcherDeadState(this, stateMachine, "Dead", this);
        archerAttackState = new PlayerArcherAttackState(this, stateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(archerIdleState);
    }

    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0)
            stateMachine.ChangeState(archerDeadState);
    }
    public override void AnimationArcherAttack()
    {
        base.AnimationArcherAttack();
        GameObject arrow = Instantiate(arrowPerfab, transform.position, Quaternion.identity);
    }
}
