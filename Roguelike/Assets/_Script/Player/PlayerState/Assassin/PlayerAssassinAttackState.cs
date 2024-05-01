using UnityEngine;

public class PlayerAssassinAttackState : PlayerAssassinGroundState
{
    public PlayerAssassinAttackState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Assassin player_Assassin) : base(player, stateMachine, animboolName, player_Assassin)
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
        player.anim.speed = player.stats.attackSpeed.GetValue();
        if (triggerCalled)
            stateMachine.ChangeState(player_Assassin.assassinIdleState);
    }
}