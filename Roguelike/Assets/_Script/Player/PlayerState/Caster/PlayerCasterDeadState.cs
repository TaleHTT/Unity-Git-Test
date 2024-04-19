public class PlayerCasterDeadState : PlayerState
{
    public PlayerCasterDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Caster player_Caster) : base(player, stateMachine, animboolName)
    {
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
    }
}
