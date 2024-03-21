using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Caster : PlayerBase
{
    [Tooltip("∑®«Ú‘§÷∆ÃÂ")]
    public GameObject OrbPerfab;
    public PlayerCasterIdleState casterIdleState {  get; private set; }
    public PlayerCasterAttackState casterAttackState {  get; private set; }
    public PlayerCasterDeadState casterDeadState {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
        casterIdleState = new PlayerCasterIdleState(this, stateMachine, "Idle", this);
        casterAttackState = new PlayerCasterAttackState(this, stateMachine, "Attack", this);
        casterDeadState = new PlayerCasterDeadState(this, stateMachine, "Dead", this);
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
    public override void AnimationCasterAttack()
    {
        base.AnimationCasterAttack();
        GameObject Orb = Instantiate(OrbPerfab, transform.position, Quaternion.identity);
    }
}
