public class PlayerIceCasterAttackState : PlayerState
{
    Player_IceCaster player_IceCaster;
    public PlayerIceCasterAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_IceCaster player_IceCaster) : base(player, stateMachine, animboolName)
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
        player.anim.speed = player.stats.attackSpeed.GetValue() + defaultAttackSpeed;
        if (triggerCalled)
            stateMachine.ChangeState(player_IceCaster.iceCasterIdleState);
    }
}