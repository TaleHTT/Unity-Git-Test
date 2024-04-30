using System;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private EnemyBase enemy => GetComponentInParent<EnemyBase>();
    Transform attackTarget;
    Enemy_Shaman enemy_Shaman => GetComponentInParent<Enemy_Shaman>();
    Enemy_Bloodsucker enemy_Bloodsucker => GetComponentInParent<Enemy_Bloodsucker>();
    Enemy_Saber_Skill_Controller enemy_Saber_Skill_Controller => GetComponentInParent<Enemy_Saber_Skill_Controller>();
    Enemy_Assassin_Skill_Controller enemy_Assassin_Skill_Controller => GetComponentInParent<Enemy_Assassin_Skill_Controller>();
    Enemy_Two_Handed_Saber_Skill_Controller enemy_Two_Handed_Saber_Skill_Controller => GetComponentInParent<Enemy_Two_Handed_Saber_Skill_Controller>();
    Enemy_Assassin enemy_Assassin => GetComponentInParent<Enemy_Assassin>();
    private void AnimationFinishTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void Update()
    {
        if (enemy_Bloodsucker != null)
        {
            if (attackTarget == null)
                attackTarget = enemy_Bloodsucker.cloestTarget;
            else
            {
                if (Vector2.Distance(transform.position, attackTarget.position) > enemy_Bloodsucker.attackRadius || attackTarget.GetComponent<EnemyStats>().currentHealth <= 0)
                    attackTarget = enemy_Bloodsucker.cloestTarget;
                else
                    return;
            }
        }
    }
    private void SaberAttackTrigger()
    {
        if (enemy.cloestTarget != null)
        {
            enemy.cloestTarget.GetComponent<IPlayerTakeDamageable>().TakeDamage(enemy.stats.damage.GetValue());
            enemy.cloestTarget.GetComponent<PlayerBase>().isHit = true;
        }
    }
    private void BloodsuckerAttackTrigger()
    {
        if (enemy_Bloodsucker.position == 0 || enemy_Bloodsucker.position == 1 || enemy_Bloodsucker.position == 2)
        {
            attackTarget.GetComponent<PlayerStats>()?.TakeDamage(enemy_Bloodsucker.stats.damage.GetValue());
            attackTarget.GetComponent<PlayerBase>().isHit = true;
            enemy_Bloodsucker.stats.damage.AddModfiers(enemy_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.normalExtraAddDamage);
            enemy_Bloodsucker.stats.TakeTreat((1 + DataManager.instance.bloodsucker_Skill_Data.normalExtraAddHp_1) * enemy_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.normalExtraAddDamage);
        }
        else if (enemy_Bloodsucker.position == 3 || enemy_Bloodsucker.position == 4 || enemy_Bloodsucker.position == 5)
        {
            enemy.AnimationBloodsuckerAttack();
        }
    }
    private void SlimeAttackTrigger()
    {
        if (enemy.cloestTarget != null)
        {
            enemy.cloestTarget.GetComponent<PlayerStats>()?.TakeDamage(enemy.stats.damage.GetValue());
            enemy.cloestTarget.GetComponent<PlayerBase>().isHit = true;
        }
    }
    private void AssassinAttackTrigger()
    {
        if (enemy.cloestTarget != null)
        {
            enemy.cloestTarget.GetComponent<PlayerBase>().isHit = true;
            if (enemy_Assassin.isStrengthen)
            {
                enemy_Assassin.assassinateTarget.GetComponent<PlayerStats>().TakeDamage(enemy_Assassin.stats.damage.GetValue() * 3);
                enemy_Assassin.assassinateTarget.GetComponent<PlayerBase>().hit_Assassin++;
                if (enemy_Assassin.assassinateTarget.GetComponent<PlayerBase>().isHunting)
                    enemy_Assassin.assassinateTarget.GetComponent<PlayerBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_2_durationTimer;
                enemy_Assassin.isStealth = false;
                enemy_Assassin.target = enemy_Assassin.assassinateTarget;
            }
            else
            {
                enemy_Assassin.cloestTarget.GetComponent<PlayerStats>().TakeDamage(enemy_Assassin.stats.damage.GetValue() * 3);
                enemy_Assassin.cloestTarget.GetComponent<PlayerBase>().hit_Assassin++;
                if (enemy_Assassin.cloestTarget.GetComponent<PlayerBase>().isHunting)
                    enemy_Assassin.cloestTarget.GetComponent<PlayerBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_2_durationTimer;
                enemy_Assassin.isStealth = false;
                enemy_Assassin.target = enemy_Assassin.cloestTarget.gameObject;
            }
        }
    }
    private void DeadDetectTrigger()
    {
        enemy_Assassin.isStrengthen = false;
    }
    private void ShamanAttackTrigger()
    {
        if (enemy.cloestTarget != null)
        {
            enemy.cloestTarget.GetComponent<PlayerStats>().TakeDamage(enemy.stats.damage.GetValue());
            enemy.cloestTarget.GetComponent<PlayerBase>().isHit = true;
            enemy_Shaman.treatTarget.GetComponent<EnemyStats>().currentHealth *= (1 + DataManager.instance.shaman_Skill_Data.normal_ExtraTreatHp) * enemy_Shaman.stats.maxHp.GetValue();
        }
    }
    private void TwoHandedSaberAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemy.attackRadius, enemy.whatIsPlayer);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                enemy.stats.currentHealth -= enemy.stats.currentHealth * DataManager.instance.two_Handed_Saber_Skill_Data.depleteHp;
                hit.GetComponent<PlayerStats>()?.TakeDamage((float)((enemy.stats.damage.baseValue) * (2 - Math.Truncate(((enemy.stats.currentHealth / enemy.stats.maxHp.GetValue()) * 10)) / 10)));
                hit.GetComponent<PlayerBase>().layersOfBleeding_Two_Handed_Saber++;
                hit.GetComponent<PlayerBase>().timer_Two_Handed_Saber_Bleed = DataManager.instance.two_Handed_Saber_Skill_Data.skill_1_DurationTimer;
                hit.GetComponent<PlayerBase>().isHit = true;
            }
        }
    }
    private void ArcherAttackTrigger()
    {
        enemy.AnimationArcherAttack();
    }
    private void CasterAttackTrigger()
    {
        enemy.AnimationCasterAttack();
    }
    private void PriestAttackTrigger()
    {
        enemy.AnimationPriestAttack();
    }
    private void IceCasterAttackTrigger()
    {
        enemy.AnimationIceCasterAttack();
    }
}
