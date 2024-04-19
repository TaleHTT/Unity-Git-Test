using UnityEngine.UI;

public class Player_TwoHandedSaber : PlayerBase
{
    public bool isAttack;
    public PlayerTwoHandedSaberIdleState twoHandedSaberIdleState {  get; set; }
    public PlayerTwoHandedSaberDeadState twoHandedSaberDeadState { get; set; }
    public  PlayerTwoHandedSaberAttackState twoHandedSaberAttackState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        twoHandedSaberIdleState = new PlayerTwoHandedSaberIdleState(this, stateMachine, "Idle", this);
        twoHandedSaberDeadState = new PlayerTwoHandedSaberDeadState(this, stateMachine, "Dead", this);
        twoHandedSaberAttackState = new PlayerTwoHandedSaberAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(twoHandedSaberIdleState);
    }
    protected override void Update()
    {
        base.Update();
    }
}
