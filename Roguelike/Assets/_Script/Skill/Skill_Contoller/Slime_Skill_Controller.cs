using System.Collections.Generic;
using UnityEngine;

public class Slime_Skill_Controller : MonoBehaviour
{
    float duration;
    float skill_1_timer;
    float skill_2_timer;
    Player_Slime player_Slime;
    int maxNumTrigger;
    List<GameObject> slimeDetect;
    List<GameObject> teamSlime;
    Dictionary<GameObject, float> maxHp = new Dictionary<GameObject, float>();
    Dictionary<GameObject, float> attackSpeed = new Dictionary<GameObject, float>();
    Dictionary<GameObject, float> damage = new Dictionary<GameObject, float>();
    Dictionary<GameObject, float> armor = new Dictionary<GameObject, float>();
    private void Awake()
    {
        teamSlime = new List<GameObject>();
        TeamSlimeDetect();
        player_Slime = GetComponent<Player_Slime>();
    }
    private void Start()
    {
        maxNumTrigger = teamSlime.Count;
        skill_1_timer = DataManager.instance.slime_Skill_Data.skill_1_CD;
        skill_2_timer = DataManager.instance.slime_Skill_Data.skill_2_CD;
        duration = DataManager.instance.slime_Skill_Data.duration;
        if (SkillManger.instance.slime_Skill.isHave_X_Equipment)
        {
            player_Slime.attackRadius *= DataManager.instance.slime_Skill_Data.skill_X_ExtraAddAttackRadius * teamSlime.Count;
            player_Slime.stats.maxHp.AddModfiers(teamSlime.Count * (player_Slime.stats.maxHp.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddHp)));
            player_Slime.stats.damage.AddModfiers(teamSlime.Count * (player_Slime.stats.damage.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddDamage)));
            player_Slime.stats.armor.AddModfiers(teamSlime.Count * (player_Slime.stats.armor.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddArmor)));
            player_Slime.stats.attackSpeed.AddModfiers(teamSlime.Count * (player_Slime.stats.attackSpeed.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_X_ExtraAddAttackSpeed)));
        }        
    }
    private void Update()
    {
        SlimeDetect();
        skill_1_timer -= Time.deltaTime;
        skill_2_timer -= Time.deltaTime;
        if(skill_1_timer < 0)
        {
            for (int i = 0; i < maxNumTrigger; i++)
            {
                RangeAttack();
            }
            skill_1_timer = DataManager.instance.slime_Skill_Data.skill_1_CD;
        }
    }
    private void FixedUpdate()
    {
        if (skill_2_timer < 0)
        {
            duration -= Time.deltaTime;
            if (duration < 0)
            {
                skill_2_timer = DataManager.instance.slime_Skill_Data.duration;
                return;
            }
            else
            {
                AddMaxHp();
                AddAttackSpeed();
                AddDamage();
                AddArmor();
            }
        }
    }
    public void AddAttackSpeed()
    {
        for (int i = 0; i < slimeDetect.Count; i++)
        {
            PlayerStats player = slimeDetect[i].GetComponent<PlayerStats>();
            if(duration > 0)
            {
                if (attackSpeed.TryGetValue(slimeDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.attackSpeed.GetValue();
                    attackSpeed.Add(slimeDetect[i], player.attackSpeed.GetValue());
                    player.attackSpeed.AddModfiers(baseValue * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
            else
            {
                if (attackSpeed.TryGetValue(slimeDetect[i], out float value))
                {
                    player.attackSpeed.RemoveModfiers(value * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
        }
    }
    public void AddMaxHp()
    {
        for (int i = 0; i < slimeDetect.Count; i++)
        {
            PlayerStats player = slimeDetect[i].GetComponent<PlayerStats>();
            if (duration > 0)
            {
                if (maxHp.TryGetValue(slimeDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.maxHp.GetValue();
                    maxHp.Add(slimeDetect[i], player.maxHp.GetValue());
                    player.maxHp.AddModfiers(baseValue * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
            else
            {
                if (maxHp.TryGetValue(slimeDetect[i], out float value))
                {
                    player.maxHp.RemoveModfiers(value * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
            player.UpdateHp();
        }
    }
    public void AddDamage()
    {
        for (int i = 0; i < slimeDetect.Count; i++)
        {
            PlayerStats player = slimeDetect[i].GetComponent<PlayerStats>();
            if (duration > 0)
            {
                if (damage.TryGetValue(slimeDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.damage.GetValue();
                    damage.Add(slimeDetect[i], player.damage.GetValue());
                    player.damage.AddModfiers(baseValue * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
            else
            {
                if (damage.TryGetValue(slimeDetect[i], out float value))
                {
                    player.damage.RemoveModfiers(value * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
        }
    }
    public void AddArmor()
    {
        for (int i = 0; i < slimeDetect.Count; i++)
        {
            PlayerStats player = slimeDetect[i].GetComponent<PlayerStats>();
            if (duration > 0)
            {
                if (armor.TryGetValue(slimeDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.armor.GetValue();
                    armor.Add(slimeDetect[i], player.armor.GetValue());
                    player.armor.AddModfiers(baseValue * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
            else
            {
                if (armor.TryGetValue(slimeDetect[i], out float value))
                {
                    player.armor.RemoveModfiers(value * DataManager.instance.slime_Skill_Data.skill_2_ExtraAddHp);
                }
            }
        }
    }
    public void RangeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Slime.attackRadius + 5);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                hit.GetComponent<EnemyStats>().TakeDamage(player_Slime.stats.maxHp.GetValue() * (1 + DataManager.instance.slime_Skill_Data.skill_1_ExtraAddHp));
        }
    }
    public void SlimeDetect()
    {
        slimeDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.slime_Skill_Data.radius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Player_Slime>() != null)
                slimeDetect.Add(hit.gameObject);
        }
    }
    public void TeamSlimeDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Player_Slime>() != null)
            {
                teamSlime.Add(hit.gameObject);
            }
        }
    }
}