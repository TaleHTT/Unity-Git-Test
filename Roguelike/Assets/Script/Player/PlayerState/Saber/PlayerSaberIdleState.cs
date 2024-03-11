using System.Collections.Generic;
using UnityEngine;

public class PlayerSaberIdleState : PlayerState
{
    public Player_Saber player_Saber;
    public PlayerSaberIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animBoolName, Player_Saber player_Saber) : base(player, stateMachine, animBoolName)
    {
        this.player_Saber = player_Saber;
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
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
            player.anim.SetBool("Attack", true);
        }
        else
        {
            player.anim.SetBool("Attack", false);
            stateMachine.ChangeState(player_Saber.saberIdleState);
        }
        if (Input.GetMouseButtonDown(0))
            stateMachine.ChangeState(player_Saber.saberMoveState);
    }
}
