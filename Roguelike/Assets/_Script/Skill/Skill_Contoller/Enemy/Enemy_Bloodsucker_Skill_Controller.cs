using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Bloodsucker_Skill_Controller : Bloodsucker_Skill_Controller
{
    public Enemy_Bloodsucker enemy_Bloodsucker => GetComponent<Enemy_Bloodsucker>();
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
        maxBlood = 5;
        maxBatNum = 1;
    }
    protected override void Update()
    {
        base.Update();
        if (enemy_Bloodsucker.cloestTarget != null)
        {
            attackDir = (enemy_Bloodsucker.cloestTarget.transform.position - transform.position).normalized;
            SkillDetect();
        }
        skill_2_Timer -= Time.deltaTime;
        if (currentBlood >= maxBlood)
        {
            if (enemy_Bloodsucker.position == 0 || enemy_Bloodsucker.position == 1 || enemy_Bloodsucker.position == 2)
            {
                if (enemy_Bloodsucker.playerDetects.Count > 0)
                    RangeDamage();
            }
            else if (enemy_Bloodsucker.position == 3 || enemy_Bloodsucker.position == 4 || enemy_Bloodsucker.position == 5)
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
            if (enemy_Bloodsucker.playerDetects.Count > 0)
            {
                if (enemy_Bloodsucker.position == 0 || enemy_Bloodsucker.position == 1 || enemy_Bloodsucker.position == 2)
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
            if (enemy_Bloodsucker.position == 3 || enemy_Bloodsucker.position == 4 || enemy_Bloodsucker.position == 5)
            {
                if (enemy_Bloodsucker.stats.isDefens == false)
                {
                    enemy_Bloodsucker.stats.defensNum = 2;
                    enemy_Bloodsucker.stats.isDefens = true;
                    parasitismBatDefensPool.Get();
                }
                duration -= Time.deltaTime;
            }
        }
    }
    public void BloodAdd()
    {
        enemy_Bloodsucker.stats.currentHealth -= enemy_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraRemoveHp;
        currentBlood++;
    }
    public void SkillDetect()
    {
        skillDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.bloodsucker_Skill_Data.length);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
                skillDetect.Add(hit.gameObject);
        }
    }
    public void RangeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, enemy_Bloodsucker.attackRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                    hit.GetComponent<PlayerStats>().TakeDamage(enemy_Bloodsucker.stats.maxHp.GetValue() * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage));
            }
        }
        currentBlood -= 5;
    }
    private GameObject CreatRectangleFunc()
    {
        var rect = Instantiate(rectanglePrefab, transform.position, Quaternion.identity);
        rect.transform.position = new Vector2(transform.position.x + DataManager.instance.bloodsucker_Skill_Data.length / 2, transform.position.y);
        rect.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg);
        rect.GetComponent<Enemy_RectangleDamage_Controller>().rectanglePool = rectanglePool;
        rect.GetComponent<Enemy_RectangleDamage_Controller>().damage = enemy_Bloodsucker.batPrefab.GetComponent<Enemy_Bat_Controller>().damage * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage);
        return rect;
    }
    public GameObject CreateParasitismBatDefensFunc()
    {
        var bat = Instantiate(parasitismBatDefensPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<Enemy_ParasitismBatDefens_Controller>().parasitismBatDefensPool = parasitismBatDefensPool;
        bat.GetComponent<Enemy_ParasitismBatDefens_Controller>().enemy_Bloodsucker_Skill_Controller = this;
        return bat;
    }
    public GameObject CreateParasitismBatAttackFunc()
    {
        var bat = Instantiate(parasitismBatAttackPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<Enemy_ParasitismBatAttack_Controller>().parasitismBatPool = parasitismBatAttackPool;
        bat.GetComponent<Enemy_ParasitismBatAttack_Controller>().enemy_Bloodsucker_Skill_Controller = this;
        return bat;
    }
}