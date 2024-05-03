using System;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    Transform attackTarget;
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    Player_Shaman player_Shaman => GetComponentInParent<Player_Shaman>();
    Player_Bloodsucker player_Bloodsucker => GetComponentInParent<Player_Bloodsucker>();
    Player_Saber_Skill_Controller player_Saber_Skill_Controller => GetComponentInParent<Player_Saber_Skill_Controller>();
    Player_Assassin_Skill_Controller player_Assassin_Skill_Controller => GetComponentInParent<Player_Assassin_Skill_Controller>();
    Player_Two_Handed_Saber_Skill_Controller player_Two_Handed_Saber_Skill_Controller => GetComponentInParent<Player_Two_Handed_Saber_Skill_Controller>();
    Player_Assassin player_Assassin => GetComponentInParent<Player_Assassin>();
    private void Update()
    {
        if (player_Bloodsucker != null)
        {
            if (attackTarget == null)
                attackTarget = player_Bloodsucker.closetEnemy;
            else
            {
                if (Vector2.Distance(transform.position, attackTarget.position) > player_Bloodsucker.attackRadius || attackTarget.GetComponent<EnemyStats>().currentHealth <= 0)
                    attackTarget = player_Bloodsucker.closetEnemy;
                else
                    return;
            }
        }
    }
    private void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();
    }
    private void SaberAttackTrigger()
    {
        if (player.closetEnemy != null && SkillManger.instance.saber_Skill.isHave_X_Equipment == false)
        {
            player.closetEnemy.GetComponent<EnemyStats>()?.TakeDamage(player.stats.damage.GetValue());
            player.closetEnemy.GetComponent<EnemyBase>().isHit = true;
        }

        else if (player.closetEnemy != null && SkillManger.instance.saber_Skill.isHave_X_Equipment == true && player_Saber_Skill_Controller.saberDetect.Count == 1 && player_Saber_Skill_Controller.isZeroPosition == true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.attackRadius, player.whatIsEnemy);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<EnemyBase>() != null)
                {
                    hit.GetComponent<EnemyStats>()?.TakeDamage(player.stats.damage.GetValue());
                    hit.GetComponent<EnemyBase>().isHit = true;
                }
            }
        }
    }
    private void BloodsuckerAttackTrigger()
    {
        if (player_Bloodsucker.position == 0 || player_Bloodsucker.position == 1 || player_Bloodsucker.position == 2)
        {
            attackTarget.GetComponent<EnemyStats>()?.TakeDamage(player_Bloodsucker.stats.damage.GetValue());
            attackTarget.GetComponent<EnemyBase>().isHit = true;
            player_Bloodsucker.stats.damage.AddModfiers(player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.normalExtraAddDamage);
            player_Bloodsucker.stats.TakeTreat((1 + DataManager.instance.bloodsucker_Skill_Data.normalExtraAddHp_1) * player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.normalExtraAddDamage);
        }
        else if (player_Bloodsucker.position == 3 || player_Bloodsucker.position == 4 || player_Bloodsucker.position == 5)
        {
            player.AnimationBloodsuckerAttack();
        }
    }
    private void SlimeAttackTrigger()
    {
        if (player.closetEnemy != null)
        {
            player.closetEnemy.GetComponent<EnemyStats>()?.TakeDamage(player.stats.damage.GetValue());
            player.closetEnemy.GetComponent<EnemyBase>().isHit = true;
        }
    }
    private void AssassinAttackTrigger()
    {
        if (player.closetEnemy != null)
        {
            player.closetEnemy.GetComponent<EnemyBase>().isHit = true;
            if (player_Assassin.isStrengthen)
            {
                player_Assassin.assassinateTarget.GetComponent<EnemyStats>().TakeDamage(player_Assassin.stats.damage.GetValue() * 3);
                player_Assassin.assassinateTarget.GetComponent<EnemyBase>().hit_Assassin++;
                if (player_Assassin.assassinateTarget.GetComponent<EnemyBase>().isHunting)
                    player_Assassin.assassinateTarget.GetComponent<EnemyBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_2_durationTimer;
                player_Assassin.isStealth = false;
                player_Assassin.target = player_Assassin.assassinateTarget;
            }
            else
            {
                player_Assassin.closetEnemy.GetComponent<EnemyStats>().TakeDamage(player_Assassin.stats.damage.GetValue() * 3);
                player_Assassin.closetEnemy.GetComponent<EnemyBase>().hit_Assassin++;
                if (player_Assassin.closetEnemy.GetComponent<EnemyBase>().isHunting)
                    player_Assassin.closetEnemy.GetComponent<EnemyBase>().markDurationTimer = DataManager.instance.assassin_Skill_Data.skill_2_durationTimer;
                player_Assassin.isStealth = false;
                player_Assassin.target = player_Assassin.closetEnemy.gameObject;
            }
        }
    }
    private void DeadDetectTrigger()
    {
        player_Assassin.DeadDetect();
        player_Assassin.isStrengthen = false;
    }
    private void ShamanAttackTrigger()
    {
        if (player.closetEnemy != null)
        {
            player.closetEnemy.GetComponent<EnemyStats>().TakeDamage(player.stats.damage.GetValue());
            player.closetEnemy.GetComponent<EnemyBase>().isHit = true;
            player_Shaman.treatTarget.GetComponent<PlayerStats>().currentHealth *= (1 + DataManager.instance.shaman_Skill_Data.normal_ExtraTreatHp) * player_Shaman.stats.maxHp.GetValue();
        }
    }
    private void TwoHandedSaberAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.attackRadius, player.whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                player.stats.currentHealth -= player.stats.currentHealth * DataManager.instance.two_Handed_Saber_Skill_Data.depleteHp;
                hit.GetComponent<EnemyStats>()?.TakeDamage((float)((player.stats.damage.baseValue) * (2 - Math.Truncate(((player.stats.currentHealth / player.stats.maxHp.GetValue()) * 10)) / 10)));
                hit.GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber++;
                hit.GetComponent<EnemyBase>().timer_Two_Handed_Saber_Bleed = DataManager.instance.two_Handed_Saber_Skill_Data.skill_1_DurationTimer;
                hit.GetComponent<EnemyBase>().isHit = true;
                if (SkillManger.instance.two_Handed_Saber_Skill.isHave_X_Equipment == true)
                    player_Two_Handed_Saber_Skill_Controller.numOfAttacks++;
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
    private void IceCasterAttackTrigger()
    {
        player.AnimationIceCasterAttack();
    }
}
