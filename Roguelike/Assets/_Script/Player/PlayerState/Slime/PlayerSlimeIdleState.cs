public class PlayerSlimeIdleState : PlayerState
{
    Player_Slime player_Slime;
    public PlayerSlimeIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Slime player_Slime) : base(player, stateMachine, animboolName)
    {
        this.player_Slime = player_Slime;
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
        if (player_Slime.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Slime.slimeAttackState);
    }
}