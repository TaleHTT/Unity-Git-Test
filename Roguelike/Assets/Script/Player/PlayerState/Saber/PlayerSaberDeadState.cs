public class PlayerSaberDeadState : PlayerState
{
    public Player_Saber player_Saber;
    public PlayerSaberDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Saber player_Saber) : base(player, stateMachine, animboolName)
    {
        this.player_Saber = player_Saber;
    }

    public override void Enter()
    {
        base.Enter();
        player.stats.attackRadius.baseValue = 0;
        player.cd.enabled = false;
        player.enemyDetects.Clear();
        player.anim.SetBool("Attack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
    }
}
