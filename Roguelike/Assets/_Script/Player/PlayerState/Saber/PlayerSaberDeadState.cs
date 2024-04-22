public class PlayerSaberDeadState : PlayerState
{
    Player_Saber player_Saber;
    public PlayerSaberDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animBoolName, Player_Saber player_Saber) : base(player, stateMachine, animBoolName)
    {
        this.player_Saber = player_Saber;
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
        if (player_Saber.isDead == false)
            stateMachine.ChangeState(player_Saber.saberIdleState);
    }
}
