public class Enemy_Summons_Hound : Enemy_Summons_Base
{
    public Enemy_Summons_StateMachine stateMachine { get; set; }
    public Enemy_Summons_Hound_IdleState houndIdleState { get; set; }
    public Enemy_Summons_Hound_MoveState houndMoveState { get; set; }
    public Enemy_Summons_Hound_DeadState houndDeadState { get; set; }
    public Enemy_Summons_Hound_ChaseState houndChaseState { get; set; }
    public Enemy_Summons_Hound_AttackState houndAttackState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new Enemy_Summons_StateMachine();
        houndIdleState = new Enemy_Summons_Hound_IdleState(this, stateMachine, "Idle", this);
        houndMoveState = new Enemy_Summons_Hound_MoveState(this, stateMachine, "Move", this);
        houndDeadState = new Enemy_Summons_Hound_DeadState(this, stateMachine, "Dead", this);
        houndChaseState = new Enemy_Summons_Hound_ChaseState(this, stateMachine, "Move", this);
        houndAttackState = new Enemy_Summons_Hound_AttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        stateMachine.Initialize(houndIdleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
}