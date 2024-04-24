public class PlayerSaberAttackState : PlayerState
{
    Player_Saber player_Saber;
    public PlayerSaberAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animBoolName, Player_Saber player_Saber) : base(player, stateMachine, animBoolName )
    {
        this.player_Saber = player_Saber;
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
            stateMachine.ChangeState(player_Saber.saberIdleState);
    }
}
