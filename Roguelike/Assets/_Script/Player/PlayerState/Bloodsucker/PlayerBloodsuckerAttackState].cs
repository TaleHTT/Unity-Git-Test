public class PlayerBloodsuckerAttackState : PlayerState
{
    Player_Bloodsucker player_Bloodsucker;
    public PlayerBloodsuckerAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Bloodsucker player_Bloodsucker) : base(player, stateMachine, animboolName)
    {
        this.player_Bloodsucker = player_Bloodsucker;
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
        player.anim.speed = player.stats.attackSpeed.GetValue() + defaultAttackSpeed;
        if (player.enemyDetects.Count <= 0)
            stateMachine.ChangeState(player_Bloodsucker.bloodsuckerIdleState);
    }
}