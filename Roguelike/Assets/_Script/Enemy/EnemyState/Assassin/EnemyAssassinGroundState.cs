using UnityEngine;

public class EnemyAssassinGroundState : EnemyState
{
    public Enemy_Assassin enemy_Assassin;
    public EnemyAssassinGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Assassin enemy_Assassin) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Assassin = enemy_Assassin;
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
        if (enemy_Assassin.isStealth == true)
        {
            enemy_Assassin.durationTimer -= Time.deltaTime;
            if (enemy_Assassin.durationTimer <= 0)
            {
                enemy_Assassin.isStrengthen = true;
                enemy_Assassin.isStealth = false;
                enemy_Assassin.deadTimer = DataManager.instance.assassin_Skill_Data.skill_1_durationTimer;
                stateMachine.ChangeState(enemy_Assassin.assassinIdleState);
            }
        }
        if (enemy_Assassin.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Assassin.assassinChaseState);
        if (enemy.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Assassin.assassinAttackState);
        if (enemy_Assassin.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_Assassin.assassinDeadState);
    }
}
