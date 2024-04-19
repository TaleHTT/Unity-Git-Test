public class Player_Summons_Hound : Player_Summons_Base
{
    public Player_Summons_StateMachine stateMachine { get; set; }
    public Player_Summons_Hound_IdleState houndIdleState { get; set; }
    public Player_Summons_Hound_MoveState houndMoveState { get; set; }
    public Player_Summons_Hound_DeadState houndDeadState { get; set; }
    public Player_Summons_Hound_ChaseState houndChaseState { get; set; }
    public Player_Summons_Hound_AttackState houndAttackState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new Player_Summons_StateMachine();
        houndIdleState = new Player_Summons_Hound_IdleState(this, stateMachine, "Idle");
        houndMoveState = new Player_Summons_Hound_MoveState(this, stateMachine, "Move");
        houndDeadState = new Player_Summons_Hound_DeadState(this, stateMachine, "Dead");
        houndChaseState = new Player_Summons_Hound_ChaseState(this, stateMachine, "Move");
        houndAttackState = new Player_Summons_Hound_AttackState(this, stateMachine, "Attack");
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

    public void DamageEffect()
    {

    }
}