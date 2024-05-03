using UnityEngine;

public class PlayerSaberIdleState : PlayerState
{
    Player_Saber player_Saber;
    public PlayerSaberIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animBoolName, Player_Saber player_Saber) : base(player, stateMachine, animBoolName)
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
        if (player_Saber.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Saber.saberAttackState);
    }
}
