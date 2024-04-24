public class PlayerArcherAttackState : PlayerState
{
    private Player_Archer player_Archer;
    public PlayerArcherAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Archer player_Archer) : base(player, stateMachine, animboolName)
    {
        this.player_Archer = player_Archer;
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
            stateMachine.ChangeState(player_Archer.archerIdleState);
    }
}
