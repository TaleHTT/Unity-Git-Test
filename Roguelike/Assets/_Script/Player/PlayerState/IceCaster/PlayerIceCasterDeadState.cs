public class PlayerIceCasterDeadState : PlayerState
{
    Player_IceCaster player_IceCaster;
    public PlayerIceCasterDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_IceCaster player_IceCaster) : base(player, stateMachine, animboolName)
    {
        this.player_IceCaster = player_IceCaster;
    }

    public override void Enter()
    {
        base.Enter();
        DeadLogic();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player_IceCaster.isDead == false)
            stateMachine.ChangeState(player_IceCaster.iceCasterIdleState);
    }
}