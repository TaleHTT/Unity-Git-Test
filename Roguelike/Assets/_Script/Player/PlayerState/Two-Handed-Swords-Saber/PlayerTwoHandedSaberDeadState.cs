public class PlayerTwoHandedSaberDeadState : PlayerState
{
    Player_TwoHandedSaber player_TwoHandedSaber;
    public PlayerTwoHandedSaberDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_TwoHandedSaber player_TwoHandedSaber) : base(player, stateMachine, animboolName)
    {
        this.player_TwoHandedSaber = player_TwoHandedSaber;
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