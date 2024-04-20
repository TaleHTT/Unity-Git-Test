public class Player_Slime : PlayerBase
{
    public PlayerSlimeDeadState slimeDeadState { get; set; }
    public PlayerSlimeIdleState slimeIdleState { get; set; }
    public PlayerSlimeAttackState slimeAttackState {  get; set; }
    protected override void Awake()
    {
        base.Awake();
        slimeIdleState = new PlayerSlimeIdleState(this, stateMachine, "Idle", this);
        slimeDeadState = new PlayerSlimeDeadState(this, stateMachine, "Dead", this);
        slimeAttackState = new PlayerSlimeAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.ChangeState(slimeIdleState);
    }
    protected override void Update()
    {
        base.Update();
    }
}