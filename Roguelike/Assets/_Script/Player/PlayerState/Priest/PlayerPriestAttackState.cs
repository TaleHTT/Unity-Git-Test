public class PlayerPriestAttackState : PlayerState
{
    Player_Priest player_Priest;
    public PlayerPriestAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Priest player_Priest) : base(player, stateMachine, animboolName)
    {
        this.player_Priest = player_Priest;
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
            stateMachine.ChangeState(player_Priest.priestIdleState);
    }
}
