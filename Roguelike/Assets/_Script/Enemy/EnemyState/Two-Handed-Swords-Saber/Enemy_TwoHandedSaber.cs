using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Enemy_TwoHandedSaber : EnemyBase
{
    public bool isAttack;
    public float cdTimer;
    public Two_Handed_Saber_Skill_Controller two_Handed_Saber_Skill_Controller { get; set; }
    public EnemyTwoHandedSaberIdleState twoHandedSaberIdleState { get; private set; }
    public EnemyTwoHandedSaberDeadState twoHandedSaberDeadState { get; private set; }
    public EnemyTwoHandedSaberChaseState twoHandedSaberChaseState { get; private set; }
    public EnemyTwoHandedSaberAttackState twoHandedSaberAttackState { get; private set; }
    public EnemyTwoHandedSaberPatrolState twoHandedSaberPatrolState { get; private set; }
    public EnemyTwoHandedSaberStormBladesState twoHandedSaberStormBladesState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        twoHandedSaberDeadState = new EnemyTwoHandedSaberDeadState(this, stateMachine, "Dead", this);
        twoHandedSaberIdleState = new EnemyTwoHandedSaberIdleState(this, stateMachine, "Idle", this);
        twoHandedSaberChaseState = new EnemyTwoHandedSaberChaseState(this, stateMachine, "Move", this);
        twoHandedSaberPatrolState = new EnemyTwoHandedSaberPatrolState(this, stateMachine, "Move", this);
        twoHandedSaberAttackState = new EnemyTwoHandedSaberAttackState(this, stateMachine, "Attack", this);
        twoHandedSaberStormBladesState = new EnemyTwoHandedSaberStormBladesState(this, stateMachine, "Skill", this);
    }
    protected override void Start()
    {
        base.Start();
        cdTimer = DataManager.instance.two_Handed_Saber_Skill_Data.CD;
        stateMachine.Initialize(twoHandedSaberIdleState);
    }
    protected override void Update()
    {
        base.Update();
        cdTimer -= Time.deltaTime;
        if (stats.currentHealth / stats.maxHp.GetValue() < 0.5f && cdTimer <= 0 && stats.isUseSkill == false)
            stateMachine.ChangeState(twoHandedSaberStormBladesState);
    }
}
