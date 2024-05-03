public class PlayerArcherIdleState : PlayerState
{
    private Player_Archer player_Archer;
    public PlayerArcherIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Archer player_Archer) : base(player, stateMachine, animboolName)
    {
        this.player_Archer = player_Archer;
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
        if (player.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Archer.archerAttackState);
    }
}
