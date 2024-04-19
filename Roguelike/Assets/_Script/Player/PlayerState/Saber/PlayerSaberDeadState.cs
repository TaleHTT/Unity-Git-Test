public class PlayerSaberDeadState : PlayerState
{
    public PlayerSaberDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Saber player_Saber) : base(player, stateMachine, animboolName)
    {
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
