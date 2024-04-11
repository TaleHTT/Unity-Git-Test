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
        if (enemy_Archer.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Archer.archerChaseState);
        if (enemy.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Archer.archerAttackState);
    }
}
