using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherDeadState : PlayerState
{
    public Player_Archer player_Archer;
    public PlayerArcherDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Archer player_Archer) : base(player, stateMachine, animboolName)
    {
        this.player_Archer = player_Archer;
    }

    public override void Enter()
    {
        base.Enter();
        player.stats.attackRadius.baseValue = 0;
        player.cd.enabled = false;
        player.enemyDetects.Clear();
        player.anim.SetBool("Attack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(0, 0);
    }
}
