using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPriestIdleState : PlayerState
{
    Player_Priest player_Priest;
    public PlayerPriestIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Priest player_Priest) : base(player, stateMachine, animboolName)
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
            player.stateMachine.ChangeState(player_Priest.priestIdleState);
    }
}
