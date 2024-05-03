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
        stateTimer = 2f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            Heal();
            stateTimer = 1f;
        }
        if (player_Slime.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Slime.slimeAttackState);
    }
}