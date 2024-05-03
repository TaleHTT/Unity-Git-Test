using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_Bloodsucker_Skill_Controller : Bloodsucker_Skill_Controller
{
    public Player_Bloodsucker player_Bloodsucker => GetComponent<Player_Bloodsucker>();
    protected override void Awake()
    {
        base.Awake();
        rectanglePool = new ObjectPool<GameObject>(CreatRectangleFunc, ActionRectangleOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        parasitismBatAttackPool = new ObjectPool<GameObject>(CreateParasitismBatAttackFunc, ActionParasitismBatAttackOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        parasitismBatDefensPool = new ObjectPool<GameObject>(CreateParasitismBatDefensFunc, ActionParasitismBatDefensOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        if (SkillManger.instance.bloodsucker_Skill.isHave_X_Equipment)
        {
            maxBlood = Mathf.Infinity;
            maxBatNum = 2;
        }
        else
        {
            maxBlood = 5;
            maxBatNum = 1;
        }
    }
    protected override void Update()
    {
        base.Update();
        if (player_Bloodsucker.closetEnemy != null)
        {
            attackDir = (player_Bloodsucker.closetEnemy.transform.position - transform.position).normalized;
            SkillDetect();
        }
        skill_2_Timer -= Time.deltaTime;
        if (currentBlood >= maxBlood)
        {
            if (player_Bloodsucker.position == 0 || player_Bloodsucker.position == 1 || player_Bloodsucker.position == 2)
            {
                if (player_Bloodsucker.enemyDetects.Count > 0)
                    RangeDamage();
            }
            else if (player_Bloodsucker.position == 3 || player_Bloodsucker.position == 4 || player_Bloodsucker.position == 5)
            {
                if (skillDetect.Count > 0)
                {
                    currentBlood -= 5;
                    rectanglePool.Get();
                }
            }
            return;
        }
        else
        {
            indexTimer -= Time.deltaTime;
            if (indexTimer < 0)
            {
                BloodAdd();
                indexTimer = DataManager.instance.bloodsucker_Skill_Data.indexTimer;
            }
        }
        if (skill_2_Timer < 0)
        {
            if (player_Bloodsucker.enemyDetects.Count > 0)
            {
                if (player_Bloodsucker.position == 0 || player_Bloodsucker.position == 1 || player_Bloodsucker.position == 2)
                {
                    duration -= Time.deltaTime;
                    if (duration > 0)
                    {
                        if (parasitismBatNum >= maxBatNum)
                            return;
                        else
                        {
                            index -= Time.deltaTime;
                            if (index < 0)
                            {
                                parasitismBatAttackPool.Get();
                                parasitismBatNum++;
                                index = 0.2f;
                            }
                        }
                    }
                    else
                    {
                        skill_2_Timer = DataManager.instance.bloodsucker_Skill_Data.skill_2_CD;
                        duration = DataManager.instance.bloodsucker_Skill_Data.skill_2_Duration;
                    }
                }
            }
            if (player_Bloodsucker.position == 3 || player_Bloodsucker.position == 4 || player_Bloodsucker.position == 5)
            {
                if (player_Bloodsucker.stats.isDefens == false)
                {
                    player_Bloodsucker.stats.defensNum = 2;
                    player_Bloodsucker.stats.isDefens = true;
                    parasitismBatDefensPool.Get();
                }
                duration -= Time.deltaTime;
            }
        }
    }
    public void BloodAdd()
    {
        if (SkillManger.instance.bloodsucker_Skill.isHave_X_Equipment)
            player_Bloodsucker.stats.currentHealth -= player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraRemoveHp * 0.5f;
        else
            player_Bloodsucker.stats.currentHealth -= player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraRemoveHp;
        currentBlood++;
    }
    public void SkillDetect()
    {
        skillDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.bloodsucker_Skill_Data.length, player_Bloodsucker.whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                skillDetect.Add(hit.gameObject);
        }
    }
    public void RangeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Bloodsucker.attackRadius, player_Bloodsucker.whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                if (SkillManger.instance.bloodsucker_Skill.isHave_X_Equipment)
                    hit.GetComponent<EnemyStats>().TakeDamage(player_Bloodsucker.stats.maxHp.GetValue() * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage) + hit.GetComponent<EnemyStats>().armor.GetValue() * 0.5f);
                else
                    hit.GetComponent<EnemyStats>().TakeDamage(player_Bloodsucker.stats.maxHp.GetValue() * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage));
            }
        }
        currentBlood -= 5;
    }
    private GameObject CreatRectangleFunc()
    {
        var rect = Instantiate(rectanglePrefab, transform.position, Quaternion.identity);
        rect.transform.position = new Vector2(transform.position.x + DataManager.instance.bloodsucker_Skill_Data.length / 2, transform.position.y);
        rect.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg);
        rect.GetComponent<Player_RectangleDamage_Controller>().rectanglePool = rectanglePool;
        rect.GetComponent<Player_RectangleDamage_Controller>().damage = player_Bloodsucker.batPrefab.GetComponent<Player_Bat_Controller>().damage * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage);
        return rect;
    }
    public GameObject CreateParasitismBatDefensFunc()
    {
        var bat = Instantiate(parasitismBatDefensPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<Player_ParasitismBatDefens_Controller>().parasitismBatDefensPool = parasitismBatDefensPool;
        bat.GetComponent<Player_ParasitismBatDefens_Controller>().player_Bloodsucker_Skill_Controller = this;
        return bat;
    }
    public GameObject CreateParasitismBatAttackFunc()
    {
        var bat = Instantiate(parasitismBatAttackPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<Player_ParasitismBatAttack_Controller>().parasitismBatPool = parasitismBatAttackPool;
        bat.GetComponent<Player_ParasitismBatAttack_Controller>().player_Bloodsucker_Skill_Controller = this;
        return bat;
    }
}