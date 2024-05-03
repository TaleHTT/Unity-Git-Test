public class PlayerBloodsuckerIdleState : PlayerState
{
    Player_Bloodsucker player_Bloodsucker;
    public PlayerBloodsuckerIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Bloodsucker player_Bloodsucker) : base(player, stateMachine, animboolName)
    {
        this.player_Bloodsucker = player_Bloodsucker;
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
        if (player_Bloodsucker.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Bloodsucker.bloodsuckerAttackState);
    }
}