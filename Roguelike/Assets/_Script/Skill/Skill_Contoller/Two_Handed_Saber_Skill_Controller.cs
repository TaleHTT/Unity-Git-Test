using System;
using System.Collections.Generic;
using UnityEngine;

public class Two_Handed_Saber_Skill_Controller : MonoBehaviour
{
    public List<GameObject> bleedDetect;
    public List<GameObject> enemyDetect;
    public float timer = 1;
    public int numOfAttacks;
    private float addSpeed;
    private float baseValue;
    Player_TwoHandedSaber player_TwoHandedSaber;
    private void Awake()
    {
        bleedDetect = new List<GameObject>();
        player_TwoHandedSaber = GetComponent<Player_TwoHandedSaber>();
    }
    private void Start()
    {
        baseValue = player_TwoHandedSaber.stats.attackSpeed.GetValue();
    }
    private void Update()
    {
        BleedDetect();
        if (player_TwoHandedSaber.stats.isUseSkill)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_TwoHandedSaber.attackRadius * 2, player_TwoHandedSaber.whatIsEnemy);
                foreach(var hit in colliders)
                {
                    if(hit.GetComponent<EnemyBase>() != null)
                    {
                        for(int i = 0; i < DataManager.instance.two_Handed_Saber_Skill_Data.times; i++)
                        {
                            hit.GetComponent<EnemyStats>().TakeDamage((float)((player_TwoHandedSaber.stats.damage.baseValue) * (2 - Math.Truncate(((player_TwoHandedSaber.stats.currentHealth / player_TwoHandedSaber.stats.maxHp.GetValue()) * 10)) / 10)) * (1 + DataManager.instance.two_Handed_Saber_Skill_Data.extraAddDamage));
                            hit.GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber++;
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, player_TwoHandedSaber.whatIsEnemy);
        foreach (var bleed in colliders)
        {
            if (bleed.GetComponent<EnemyBase>() != null)
            {
                if (bleed.GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber > 0)
                    bleedDetect.Add(bleed.gameObject);
            }
        }
    }
    public void TreatHp()
    {
        for (int i = 0; i < bleedDetect.Count; i++)
        {
            if (bleedDetect[i].GetComponent<EnemyBase>().isDead == true)
            {
                player_TwoHandedSaber.stats.currentHealth *= (1 + DataManager.instance.two_Handed_Saber_Skill_Data.recoverHp);
                bleedDetect.Remove(bleedDetect[i]);
            }
            else if (bleedDetect[i].GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber <= 0)
                bleedDetect.Remove(bleedDetect[i]);
        }
    }
    public void AddAttackSpeed()
    {
        float sum;
        float value_Num;
        float value_EnemyNum;
        value_Num = baseValue * DataManager.instance.two_Handed_Saber_Skill_Data.num_ExtraAddAttackSpeed * numOfAttacks;
        value_EnemyNum = baseValue * DataManager.instance.two_Handed_Saber_Skill_Data.enemy_ExtraAddAttackSpeed * player_TwoHandedSaber.enemyDetects.Count;
        sum = value_EnemyNum + value_Num;
        if (sum != addSpeed)
        {
            player_TwoHandedSaber.stats.attackSpeed.RemoveModfiers(addSpeed);
            addSpeed = sum;
            player_TwoHandedSaber.stats.attackSpeed.AddModfiers(sum);
        }
    }
}