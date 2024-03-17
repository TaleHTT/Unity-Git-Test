using System.Collections.Generic;
using UnityEngine;

public class EnemyArcherGroundState : EnemyState
{
    public Enemy_Archer enemy_Archer;
    public EnemyArcherGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Archer = enemy_Archer;
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
        Attack();
    }
    public void Attack()
    {
        enemy.detectTimer -= Time.deltaTime;
        if (enemy.detectTimer > 0)
        {
            enemy.detectTimer = 1;
            return;
        }
        enemy.attackDetects = new List<GameObject>();
        var colliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.stats.attackRadius.GetValue(), enemy.whatIsEnemy);
        foreach (var player in colliders)
        {
            enemy.attackDetects.Add(player.gameObject);
        }
        if (enemy.attackDetects.Count > 0)
        {
            stateMachine.ChangeState(enemy_Archer.archerAttackState);
        }
    }
}
