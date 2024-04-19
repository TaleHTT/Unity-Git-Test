public class PlayerShamanDeadState : PlayerState
{
    Player_Shaman player_Shaman;
    public PlayerShamanDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Shaman player_Shaman) : base(player, stateMachine, animboolName)
    {
        this.player_Shaman = player_Shaman;
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
    }
}