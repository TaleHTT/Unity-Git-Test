using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IceCaster_Skill_Controller : IceCaster_Skill_Controller
{
    private Enemy_IceCaster enemy_IceCaster;
    protected override void Awake()
    {
        base.Awake();
        enemy_IceCaster = GetComponent<Enemy_IceCaster>();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        SectorEnemyDetect();
        if (skill_1_Tiemr < 0)
        {
            if (enemy_IceCaster.playerDetects.Count > 0)
            {
                CreatCircleDamage();
                skill_1_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_1_CD;
            }
        }
        if (skill_2_Tiemr < 0)
        {
            if (attackDetect.Count > 0)
            {
                durationTimer -= Time.deltaTime;
                if (durationTimer < 0)
                {
                    durationTimer = DataManager.instance.iceCasterSkill_Data.skill_1_durationTimer;
                    skill_2_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_2_CD;
                    return;
                }
                else
                {
                    timer -= Time.deltaTime;
                    if (timer < 0)
                    {
                        Instantiate(effect, transform.position, Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(cloestTarget.transform.position.y - transform.position.y, cloestTarget.transform.position.x - transform.position.x)));
                        SectorDamage();
                        timer = DataManager.instance.iceCasterSkill_Data.skill_1_timer;
                        StartCoroutine(DestoryEffect());
                    }
                }
            }
        }
    }
    public void SectorDamage()
    {
        for (int i = 0; i < attackDetect.Count; i++)
        {
            attackDetect[i].GetComponent<PlayerStats>().AuthenticTakeDamage(enemy_IceCaster.stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_1_ExtraAddDamage));
            attackDetect[i].GetComponent<PlayerBase>().layerOfCold++;
        }
    }
    public void SectorEnemyDetect()
    {
        attackDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_2_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
                attackDetect.Add(hit.gameObject);
            SectorEnemyTarget();
        }
    }
    public void CreatCircleDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_1_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                hit.GetComponent<PlayerStats>().AuthenticTakeDamage(enemy_IceCaster.stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_2_ExtraAddDamage));
                hit.GetComponent<PlayerBase>().layerOfCold++;
            }
        }
    }
}