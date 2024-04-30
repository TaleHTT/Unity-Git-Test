using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Slime : EnemyBase
{
    public EnemySlimeIdleState slimeIdleState { get; private set; }
    public EnemySlimeAttackState slimeAttackState { get; private set; }
    public EnemySlimeDeadState slimeDeadState { get; private set; }
    public EnemySlimePatrolState slimePatrolState { get; private set; }
    public EnemySlimeChaseState slimeChaseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        slimeIdleState = new EnemySlimeIdleState(this, stateMachine, "Idle", this);
        slimeAttackState = new EnemySlimeAttackState(this, stateMachine, "Attack", this);
        slimeDeadState = new EnemySlimeDeadState(this, stateMachine, "Dead", this);
        slimePatrolState = new EnemySlimePatrolState(this, stateMachine, "Move", this);
        slimeChaseState = new EnemySlimeChaseState(this, stateMachine, "Move", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(slimeIdleState);
    }
    protected override void Update()
    {
        base.Update();
    }
}
