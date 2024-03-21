using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Caster : EnemyBase
{
    [Tooltip("∑®«Ú‘§÷∆ÃÂ")]
    public GameObject OrbPerfab;
    public EnemyCasterIdleState casterIdleState {  get; private set; }
    public EnemyCasterAttackState casterAttackState { get; private set; }
    public EnemyCasterDeadState casterDeadState { get; private set; }
    public EnemyCasterPatrolState casterPatrolState { get; private set; }
    public EnemyCasterChaseState casterChaseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        casterIdleState = new EnemyCasterIdleState(this, stateMachine, "Idle", this);
        casterAttackState = new EnemyCasterAttackState(this, stateMachine, "Attack", this);
        casterDeadState = new EnemyCasterDeadState(this,stateMachine, "Dead", this);
        casterPatrolState = new EnemyCasterPatrolState(this, stateMachine, "Move", this);
        casterChaseState = new EnemyCasterChaseState(this, stateMachine, "Move", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(casterIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0)
            stateMachine.ChangeState(casterDeadState);
    }
    public override void playerDetect()
    {
        base.playerDetect();
        if (playerDetects.Count > 0)
        {
            stateMachine.ChangeState(casterChaseState);
        }
    }
    public override void AnimationCasterAttack()
    {
        base.AnimationCasterAttack();
        GameObject Orb = Instantiate(OrbPerfab, transform.position, Quaternion.identity);
    }

}
