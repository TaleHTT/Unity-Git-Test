public class PlayerIceCasterIdleState : PlayerState
{
    Player_IceCaster player_IceCaster;
    public PlayerIceCasterIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_IceCaster player_IceCaster) : base(player, stateMachine, animboolName)
    {
        this.player_IceCaster = player_IceCaster;
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
        if (player_IceCaster.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_IceCaster.iceCasterAttackState);
    }
}