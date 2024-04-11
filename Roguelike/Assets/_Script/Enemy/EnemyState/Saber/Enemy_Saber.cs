public class Enemy_Saber : EnemyBase
{
    public EnemySaberIdleState saberIdleState { get; private set; }
    public EnemySaberPatrolState saberPatrolState { get; private set; }
    public EnemySaberChaseState saberChaseState { get; private set; }
    public EnemySaberDeadState saberDeadState { get; private set; }
    public EnemySaberAttackState saberAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        saberIdleState = new EnemySaberIdleState(this, stateMachine, "Idle", this);
        saberPatrolState = new EnemySaberPatrolState(this, stateMachine, "Move", this);
        saberChaseState = new EnemySaberChaseState(this, stateMachine, "Move", this);
        saberDeadState = new EnemySaberDeadState(this, stateMachine, "Dead", this);
        saberAttackState = new EnemySaberAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(saberIdleState);

    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth < 0 && isDead == false)
            stateMachine.ChangeState(saberDeadState);
    }
}
