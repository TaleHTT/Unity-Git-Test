using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class Slime_Skill_Controller : MonoBehaviour
{
    float duration;
    float skill_1_timer;
    float skill_2_timer;
    Player_Slime player_Slime;
    int maxNumTrigger;
    List<GameObject> slimeDetect;
    public int MaxNumTrigger
    {
        get
        {
            return maxNumTrigger;
        }
        set
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
            foreach(var hit in colliders)
            {
                if (hit.GetComponent<Player_Slime>() != null)
                    maxNumTrigger++;
            }
        }
    }
    private void Awake()
    {
        slimeDetect = new List<GameObject>();
        SlimeDetect();
        player_Slime = GetComponent<Player_Slime>();
        skill_1_timer = DataManager.instance.slime_Skill_Data.skill_1_CD;
        skill_2_timer = DataManager.instance.slime_Skill_Data.skill_2_CD;
        duration = DataManager.instance.slime_Skill_Data.duration;
    }
    private void Start()
    {
        if (SkillManger.instance.slime_Skill.isHave_X_Equipment)
        {
            player_Slime.stats.attackRadius *= DataManager.instance.slime_Skill_Data.skill_X_ExtraAddAttackRadius;
            player_Slime.stats.maxHp.AddModfiers(slimeDetect.Count * (player_Slime.stats.maxHp.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddHp)));
            player_Slime.stats.damage.AddModfiers(slimeDetect.Count * (player_Slime.stats.damage.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddDamage)));
            player_Slime.stats.armor.AddModfiers(slimeDetect.Count * (player_Slime.stats.armor.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddArmor)));
            player_Slime.stats.attackSpeed.AddModfiers(slimeDetect.Count * (player_Slime.stats.attackSpeed.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddAttackSpeed)));
        }        
    }
    private void Update()
    {
        skill_1_timer -= Time.deltaTime;
        skill_2_timer -= Time.deltaTime;
        if(skill_1_timer < 0)
        {
            if(MaxNumTrigger > 0)
            {
                RangeAttack();
                MaxNumTrigger--;
            }
            else
                skill_1_timer = DataManager.instance.slime_Skill_Data.skill_1_CD;
        }
    }
    public void RangeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Slime.stats.attackRadius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                hit.GetComponent<EnemyStats>().TakeDamage(player_Slime.stats.maxHp.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_1_ExtraAddHp));
        }
    }
    public void SlimeDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.slime_Skill_Data.radius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Player_Slime>() != null)
                slimeDetect.Add(hit.gameObject);
        }
    }
}