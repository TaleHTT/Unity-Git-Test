public class PlayerTwoHandedSaberAttackState : PlayerState
{
    Player_TwoHandedSaber player_TwoHandedSaber;
    public PlayerTwoHandedSaberAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_TwoHandedSaber player_TwoHandedSaber) : base(player, stateMachine, animboolName)
    {
        this.player_TwoHandedSaber = player_TwoHandedSaber;
    }

    public override void Enter()
    {
        base.Enter();
        player_TwoHandedSaber.isAttack = true;
    }

    public override void Exit()
    {
        base.Exit();
        player_TwoHandedSaber.isAttack = false;
    }

    public override void Update()
    {
        base.Update();
        player.anim.speed = player.stats.attackSpeed.GetValue() + defaultAttackSpeed;
        if (player.enemyDetects.Count <= 0)
            stateMachine.ChangeState(player_TwoHandedSaber.twoHandedSaberIdleState);
    }
}