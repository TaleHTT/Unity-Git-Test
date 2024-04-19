using System;
using UnityEngine;
public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    Player_Shaman player_Shaman => GetComponent<Player_Shaman>();
    Two_Handed_Saber_Skill_Controller two_Handed_Saber_Skill_Controller => GetComponent<Two_Handed_Saber_Skill_Controller>();
    Assassin_Skill_Controller assassin_Skill_Controller => GetComponent<Assassin_Skill_Controller>();
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
