public class PlayerCasterIdleState : PlayerState
{
    private Player_Caster player_Caster;
    public PlayerCasterIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Caster player_Caster) : base(player, stateMachine, animboolName)
    {
        this.player_Caster = player_Caster;
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
        if (player_Caster.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Caster.casterAttackState);
    }
}
