using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherIdleState : PlayerState
{
    private Player_Archer player_Archer;
    public PlayerArcherIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Archer player_Archer) : base(player, stateMachine, animboolName)
    {
        this.player_Archer = player_Archer;
    }

    public override void Enter()
    {
        base.Enter();
        //player.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.detectTimer -= Time.deltaTime;
        if (player.detectTimer > 0)
        {
            player.detectTimer = 1;
            return;
        }
        player.enemyDetects = new List<GameObject>();
        var colliders = Physics2D.OverlapCircleAll(player.transform.position, player.stats.attackRadius.GetValue(), player.whatIsEnemy);
        foreach (var enemy in colliders)
        {
            player.enemyDetects.Add(enemy.gameObject);
        }
        if (player.enemyDetects.Count > 0)
        {
            //player.anim.SetBool("Attack", true);
            stateMachine.ChangeState(player_Archer.archerAttackState);
        }
        else
        {
            //player.anim.SetBool("Attack", false);
            stateMachine.ChangeState(player_Archer.archerIdleState);
        }
        //if (Input.GetMouseButtonDown(0))
        //    stateMachine.ChangeState(player_Archer.archerMoveState);
    }
}
