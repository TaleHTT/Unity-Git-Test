using System.Collections.Generic;
using UnityEngine;

public class Player_IceCaster_Skill_Controller : IceCaster_Skill_Controller
{
    private Player_IceCaster player_IceCaster;
    protected override void Awake()
    {
        base.Awake();
        player_IceCaster = GetComponent<Player_IceCaster>();
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
            if (player_IceCaster.enemyDetects.Count > 0)
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
            attackDetect[i].GetComponent<EnemyStats>().AuthenticTakeDamage(player_IceCaster.stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_1_ExtraAddDamage));
            attackDetect[i].GetComponent<EnemyBase>().layerOfCold++;
        }
    }
    public void SectorEnemyDetect()
    {
        attackDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_2_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                attackDetect.Add(hit.gameObject);
            SectorEnemyTarget();
        }
    }
    public void CreatCircleDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_1_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                hit.GetComponent<EnemyStats>().AuthenticTakeDamage(player_IceCaster.stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_2_ExtraAddDamage));
                hit.GetComponent<EnemyBase>().layerOfCold++;
            }
        }
    }
}