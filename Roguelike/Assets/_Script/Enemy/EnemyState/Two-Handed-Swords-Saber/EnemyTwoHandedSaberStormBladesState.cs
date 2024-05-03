using UnityEngine;

public class EnemyTwoHandedSaberStormBladesState : EnemyTwoHandedSaberGroundState
{
    public EnemyTwoHandedSaberStormBladesState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_TwoHandedSaber enemy_TwoHandedSaber) : base(enemy, stateMachine, animboolName, enemy_TwoHandedSaber)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy_TwoHandedSaber.isAttack = true;
        enemy_TwoHandedSaber.stats.isUseSkill = true;
        enemy_TwoHandedSaber.cdTimer = DataManager.instance.two_Handed_Saber_Skill_Data.CD;
        stateTimer = DataManager.instance.two_Handed_Saber_Skill_Data.skill_2_DurationTimer;
    }

    public override void Exit()
    {
        base.Exit();
        enemy_TwoHandedSaber.isAttack = false;
        enemy_TwoHandedSaber.stats.isUseSkill = false;
    }

    public override void Update()
    {
        base.Update();
        enemy_TwoHandedSaber.isDead = false;
        if (enemy_TwoHandedSaber.stats.currentHealth <= 1)
            enemy_TwoHandedSaber.stats.currentHealth = 1;
    }
}
