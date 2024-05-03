public class PlayerPriestIdleState : PlayerState
{
    Player_Priest player_Priest;
    public PlayerPriestIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Priest player_Priest) : base(player, stateMachine, animboolName)
    {
        this.player_Priest = player_Priest;
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
        if (player_Priest.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Priest.priestAttackState);
    }
}
