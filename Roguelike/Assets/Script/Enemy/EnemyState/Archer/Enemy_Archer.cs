using System.Collections.Generic;
using UnityEngine;

public class Enemy_Archer : EnemyBase
{
    [Tooltip("¼ýÊ¸Ô¤ÉèÌå")]
    public GameObject arrowPerfab;
    public EnemyArcherIdleState archerIdleState { get; private set; }
    public EnemyArcherPatrolState archerMoveState { get; private set; }
    public EnemyArcherChaseState archerChaseState { get; private set; }
    public EnemyArcherDeadState archerDeadState { get; private set; }
    public EnemyArcherAttackState archerAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        archerIdleState = new EnemyArcherIdleState(this, stateMachine, "Idle", this);
        archerMoveState = new EnemyArcherPatrolState(this, stateMachine, "Move", this);
        archerChaseState = new EnemyArcherChaseState(this, stateMachine, "Move", this);
        archerDeadState = new EnemyArcherDeadState(this, stateMachine, "Dead", this);
        archerAttackState = new EnemyArcherAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(archerIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth < 0)
            stateMachine.ChangeState(archerDeadState);
    }
    public override void playerDetect()
    {
        base.playerDetect();
        if (playerDetects.Count > 0)
        {
            stateMachine.ChangeState(archerChaseState);
        }
    }
    public override void AnimationArcherAttack()
    {
        base.AnimationArcherAttack();
        GameObject arrow = Instantiate(arrowPerfab, transform.position, Quaternion.identity);
    }
    public void FirstTargetLogic()
    {
        
    }
}
