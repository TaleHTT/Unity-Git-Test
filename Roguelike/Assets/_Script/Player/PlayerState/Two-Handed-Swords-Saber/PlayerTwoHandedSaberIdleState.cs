public class PlayerTwoHandedSaberIdleState : PlayerState
{
    Player_TwoHandedSaber player_TwoHandedSaber;
    public PlayerTwoHandedSaberIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_TwoHandedSaber player_TwoHandedSaber) : base(player, stateMachine, animboolName)
    {
        this.player_TwoHandedSaber = player_TwoHandedSaber;
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
        if (player_TwoHandedSaber.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_TwoHandedSaber.twoHandedSaberAttackState);
    }
}