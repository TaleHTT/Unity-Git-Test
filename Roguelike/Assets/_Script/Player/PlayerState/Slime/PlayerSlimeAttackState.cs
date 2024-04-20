public class PlayerSlimeAttackState : PlayerState
{
    Player_Slime player_Slime;
    public PlayerSlimeAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Slime player_Slime) : base(player, stateMachine, animboolName)
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
        player.anim.speed = player.stats.attackSpeed.GetValue() + defaultAttackSpeed;
        if (player.enemyDetects.Count <= 0)
            stateMachine.ChangeState(player_Slime.slimeIdleState);
    }
}