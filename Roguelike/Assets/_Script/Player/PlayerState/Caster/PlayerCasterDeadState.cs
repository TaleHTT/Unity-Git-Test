public class PlayerCasterDeadState : PlayerState
{
    private Player_Caster player_Caster;
    public PlayerCasterDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Caster player_Caster) : base(player, stateMachine, animboolName)
    {
        this.player_Caster = player_Caster;
    }
    public override void Enter()
    {
        base.Enter();
        DeadLogic();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (player_Caster.isDead == false)
            stateMachine.ChangeState(player_Caster.casterIdleState);
    }
}
