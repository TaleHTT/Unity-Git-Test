using System.Collections.Generic;
using UnityEngine;

public class Player_Saber_Skill_Controller : Saber_Skill_Controller
{
    [HideInInspector] public Player_Saber player_Saber;
    protected override void Awake()
    {
        base.Awake();
        player_Saber = GetComponent<Player_Saber>();
    }
    protected override void Start()
    {
        base.Start();
        SaberDetect();
        if (SkillManger.instance.saber_Skill.isHave_X_Equipment == true && saberDetect.Count == 1 && isZeroPosition == true)
        {
            player_Saber.stats.damage.AddModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddDamage);
            player_Saber.stats.armor.AddModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddArmor);
            player_Saber.stats.maxHp.AddModfiers(player_Saber.stats.maxHp.GetValue() * saber_Skill_Data.extraAddHp);
            player_Saber.stats.UpdateHp();
        }
    }
    protected override void Update()
    {
        base.Update();
        if (numOfHit == saber_Skill_Data.maxNumOfHit)
        {
            CounterAttack(player_Saber.stats.armor.GetValue());
            numOfHit = 0;
        }
    }
    private void SaberDetect()
    {
        saberDetect = new List<GameObject>();
        colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var saber in colliders)
        {
            if (saber.GetComponent<Player_Saber>() != null)
                saberDetect.Add(saber.gameObject);
        }
    }
    private void CounterAttack(float counterAttackDamage)
    {
        if (SkillManger.instance.saber_Skill.isHave_X_Equipment == true && saberDetect.Count == 1 && isZeroPosition == true)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius);
        }
        else
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius * (1 + saber_Skill_Data.extraAddAttackRadius));
        }
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyStats>() != null)
            {
                hit.GetComponent<EnemyStats>().TakeDamage(saber_Skill_Data.counterBaseValue + saber_Skill_Data.extraAddArmor * counterAttackDamage);
            }
        }
    }
}