public class PlayerSaberMoveState : PlayerSaberGroundState
{
    public PlayerSaberMoveState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Saber player_Saber) : base(player, stateMachine, animboolName, player_Saber)
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
        if (player.enemyDetects.Count > 0)
        {
            stateMachine.ChangeState(player_Saber.saberAttackState);
        }
    }
}
