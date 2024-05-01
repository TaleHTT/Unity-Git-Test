using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Two_Handed_Saber_Skill_Controller : Two_Handed_Saber_Skill_Controller
{
    private Enemy_TwoHandedSaber enemy_TwoHandedSaber;
    protected override void Awake()
    {
        base.Awake();
        enemy_TwoHandedSaber = GetComponent<Enemy_TwoHandedSaber>();
    }
    protected override void Start()
    {
        base.Start();
        baseValue = enemy_TwoHandedSaber.stats.attackSpeed.GetValue();
    }
    protected override void Update()
    {
        base.Update();
        BleedDetect();
        if (enemy_TwoHandedSaber.stats.isUseSkill)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemy_TwoHandedSaber.attackRadius * 2, enemy_TwoHandedSaber.whatIsPlayer);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<PlayerBase>() != null)
                    {
                        for (int i = 0; i < DataManager.instance.two_Handed_Saber_Skill_Data.times; i++)
                        {
                            hit.GetComponent<PlayerStats>().TakeDamage((float)((enemy_TwoHandedSaber.stats.damage.baseValue) * (2 - Math.Truncate(((enemy_TwoHandedSaber.stats.currentHealth / enemy_TwoHandedSaber.stats.maxHp.GetValue()) * 10)) / 10)) * (1 + DataManager.instance.two_Handed_Saber_Skill_Data.extraAddDamage));
                            hit.GetComponent<PlayerBase>().layersOfBleeding_Two_Handed_Saber++;
                            if (SkillManger.instance.two_Handed_Saber_Skill.isHave_X_Equipment)
                                numOfAttacks++;
                        }
                    }
                }
                timer = 1;
            }
        }
        if (SkillManger.instance.two_Handed_Saber_Skill.isHave_X_Equipment)
            AddAttackSpeed();
    }
    public void BleedDetect()
    {
        TreatHp();
        bleedDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, enemy_TwoHandedSaber.whatIsPlayer);
        foreach (var bleed in colliders)
        {
            if (bleed.GetComponent<PlayerBase>() != null)
            {
                if (bleed.GetComponent<PlayerBase>().layersOfBleeding_Two_Handed_Saber > 0)
                    bleedDetect.Add(bleed.gameObject);
            }
        }
    }
    public void TreatHp()
    {
        for (int i = 0; i < bleedDetect.Count; i++)
        {
            if (bleedDetect[i].GetComponent<PlayerBase>().isDead == true)
            {
                enemy_TwoHandedSaber.stats.currentHealth *= (1 + DataManager.instance.two_Handed_Saber_Skill_Data.recoverHp);
                bleedDetect.Remove(bleedDetect[i]);
            }
            else if (bleedDetect[i].GetComponent<PlayerBase>().layersOfBleeding_Two_Handed_Saber <= 0)
                bleedDetect.Remove(bleedDetect[i]);
        }
    }
    public void AddAttackSpeed()
    {
        float sum;
        float value_Num;
        float value_EnemyNum;
        value_Num = baseValue * DataManager.instance.two_Handed_Saber_Skill_Data.num_ExtraAddAttackSpeed * numOfAttacks;
        value_EnemyNum = baseValue * DataManager.instance.two_Handed_Saber_Skill_Data.enemy_ExtraAddAttackSpeed * enemy_TwoHandedSaber.playerDetects.Count;
        sum = value_EnemyNum + value_Num;
        if (sum != addSpeed)
        {
            enemy_TwoHandedSaber.stats.attackSpeed.RemoveModfiers(addSpeed);
            addSpeed = sum;
            enemy_TwoHandedSaber.stats.attackSpeed.AddModfiers(sum);
        }
    }
}