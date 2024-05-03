public class PlayerBloodsuckerDeadState : PlayerState
{
    Player_Bloodsucker player_Bloodsucker;
    public PlayerBloodsuckerDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Bloodsucker player_Bloodsucker) : base(player, stateMachine, animboolName)
    {
        this.player_Bloodsucker = player_Bloodsucker;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player_Bloodsucker.isDead == false)
            stateMachine.ChangeState(player_Bloodsucker.bloodsuckerIdleState);
    }
}
