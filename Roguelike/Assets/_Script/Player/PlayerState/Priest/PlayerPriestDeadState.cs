public class PlayerPriestDeadState : PlayerState
{
    Player_Priest player_Priest;
    public PlayerPriestDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Priest player_Priest) : base(player, stateMachine, animboolName)
    {
        this.player_Priest = player_Priest;
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
        if (player_Priest.isDead == false)
            stateMachine.ChangeState(player_Priest.priestIdleState);
    }
}
