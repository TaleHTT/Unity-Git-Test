public class PlayerSlimeDeadState : PlayerState
{
    Player_Slime player_Slime;
    public PlayerSlimeDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Slime player_Slime) : base(player, stateMachine, animboolName)
    {
        this.player_Slime = player_Slime;
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
        if (player_Slime.isDead == false)
            stateMachine.ChangeState(player_Slime.slimeIdleState);
    }
}