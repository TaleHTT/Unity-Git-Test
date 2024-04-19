public class PlayerShamanAttackState : PlayerState
{
    Player_Shaman player_Shaman;
    public PlayerShamanAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Shaman player_Shaman) : base(player, stateMachine, animboolName)
    {
        this.player_Shaman = player_Shaman;
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
        if (player_Shaman.enemyDetects.Count < 0)
            stateMachine.ChangeState(player_Shaman.shamanIdleState);
    }
}