using System;
using UnityEngine;
public class PlayerAnimationTrigger : MonoBehaviour
{
    Transform attackTarget;
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    Player_Shaman player_Shaman => GetComponent<Player_Shaman>();
    Player_Bloodsucker player_Bloodsucker => GetComponent<Player_Bloodsucker>();
    Two_Handed_Saber_Skill_Controller two_Handed_Saber_Skill_Controller => GetComponent<Two_Handed_Saber_Skill_Controller>();
    Assassin_Skill_Controller assassin_Skill_Controller => GetComponent<Assassin_Skill_Controller>();
    private void Update()
    {
        if(player_Bloodsucker != null)
        {
            if(attackTarget == null)
                attackTarget = player_Bloodsucker.closetEnemy;
            else
            {
                if (Vector2.Distance(transform.position, attackTarget.position) > player_Bloodsucker.stats.attackRadius || attackTarget.GetComponent<EnemyStats>().currentHealth <= 0)
                    attackTarget = player_Bloodsucker.closetEnemy;
                else
                    return;
            }
        }
    }
    private void SaberAttackTrigger()
    {
        if (player.closetEnemy != null && SkillManger.instance.saber_Skill.isHave_X_Equipment == false)
            player.closetEnemy.GetComponent<EnemyStats>()?.TakeDamage(player.stats.damage.GetValue());

        else if (player.closetEnemy != null && SkillManger.instance.saber_Skill.isHave_X_Equipment == true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.stats.attackRadius, player.whatIsEnemy);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<EnemyBase>() != null)
                    hit.GetComponent<EnemyStats>()?.TakeDamage(player.stats.damage.GetValue());
            }
        }
    }
    private void BloodsuckerAttackTrigger()
    {
        if(player_Bloodsucker.position == 0 || player_Bloodsucker.position == 1 || player_Bloodsucker.position == 2)
        {
            attackTarget.GetComponent<EnemyStats>()?.TakeDamage(player_Bloodsucker.stats.maxHp.GetValue() * (1 + DataManager.instance.bloodsucker_Skill_Data.normalExtraAddDamage));
            player_Bloodsucker.stats.TakeTreat((1 + DataManager.instance.bloodsucker_Skill_Data.normalExtraAddHp_1) * player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.normalExtraAddDamage);
        }
        else if(player_Bloodsucker.position == 3 || player_Bloodsucker.position == 4 || player_Bloodsucker.position == 5)
        {
            player.AnimationBloodsuckerAttack();
        }
    }
    private void SlimeAttackTrigger()
    {
        if (player.closetEnemy != null)
            player.closetEnemy.GetComponent<EnemyStats>()?.TakeDamage(player.stats.damage.GetValue());
    }
    private void AssassinAttackTrigger()
    {
        if (player.closetEnemy != null)
        {
            player.closetEnemy.GetComponent<EnemyStats>()?.TakeDamage(player.stats.damage.GetValue());
            player.closetEnemy.GetComponent<EnemyBase>().hit_Assassin++;
            if (player.closetEnemy.GetComponent<EnemyBase>().isHunting)
                player.closetEnemy.GetComponent<EnemyBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_2_durationTimer;
            if (player.closetEnemy.GetComponent<EnemyBase>().isDead && SkillManger.instance.assassin_Skill.isHave_X_Equipment)
            {
                player.stats.currentHealth += player.stats.damage.GetValue() * (1 + DataManager.instance.assassin_Skill_Data.extraAddHp);
                assassin_Skill_Controller.num_KillEnemy++;
            }
        }
    }
    private  void ShamanAttackTrigger()
    {
        if (player.closetEnemy != null)
        {
            player.closetEnemy.GetComponent<EnemyStats>().TakeDamage(player.stats.damage.GetValue());
            player_Shaman.treatTarget.GetComponent<PlayerBase>().stats.currentHealth *= (1 + DataManager.instance.shaman_Skill_Data.normal_ExtraTreatHp) * player_Shaman.stats.maxHp.GetValue();
        }
    }
    private void TwoHandedSaberAttackTrigger()
    {
        player.stats.currentHealth -= player.stats.currentHealth * DataManager.instance.two_Handed_Saber_Skill_Data.depleteHp;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.stats.attackRadius, player.whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                hit.GetComponent<EnemyStats>()?.TakeDamage((float)((double)(player.stats.damage.GetValue()) * (1 + Math.Floor((player.stats.currentHealth / player.stats.maxHp.GetValue())))));
                hit.GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber++;
                hit.GetComponent<EnemyBase>().timer_Two_Handed_Saber = DataManager.instance.two_Handed_Saber_Skill_Data.skill_1_DurationTimer;
                if(SkillManger.instance.two_Handed_Saber_Skill.isHave_X_Equipment == true)
                    two_Handed_Saber_Skill_Controller.numOfAttack++;
            }
        }
    }
    private void ArcherAttackTrigger()
    {
        player.AnimationArcherAttack();
    }
    private void CasterAttackTrigger()
    {
        player.AnimationCasterAttack();
    }
    private void PriestAttackTrigger()
    {
        player.AnimationPriestAttack();
    }
}
