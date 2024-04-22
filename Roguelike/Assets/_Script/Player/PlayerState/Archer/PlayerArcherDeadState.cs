public class PlayerArcherDeadState : PlayerState
{
    private Player_Archer player_Archer;
    public PlayerArcherDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Archer player_Archer) : base(player, stateMachine, animboolName)
    {
        this.player_Archer = player_Archer;
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
        if (player_Archer.isDead == false)
            stateMachine.ChangeState(player_Archer.archerIdleState);
    }
}
