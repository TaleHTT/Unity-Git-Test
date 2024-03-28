using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPriestDeadState : PlayerState
{
    Player_Priest player_Priest;
    public PlayerPriestDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Priest player_Priest) : base(player, stateMachine, animboolName)
    {
        this.player_Priest = player_Priest;
    }

    public override void Enter()
    {
        base.Enter();
        player.stats.attackRadius.baseValue = 0;
        player.cd.enabled = false;
        player.enemyDetects.Clear();
        player.anim.SetBool("Attack", false);
        player.isDead = true;
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
