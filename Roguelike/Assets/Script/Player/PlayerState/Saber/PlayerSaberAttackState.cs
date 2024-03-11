public class PlayerSaberAttackState : PlayerSaberGroundState
{
    public PlayerSaberAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Saber player_Saber) : base(player, stateMachine, animboolName, player_Saber)
    {
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
        player.anim.speed = player.stats.attackSpeed.GetValue() + initialAttackSpeed;
        if (player.enemyDetects.Count <= 0)
        {
            stateMachine.ChangeState(player_Saber.saberMoveState);
        }
    }
}
