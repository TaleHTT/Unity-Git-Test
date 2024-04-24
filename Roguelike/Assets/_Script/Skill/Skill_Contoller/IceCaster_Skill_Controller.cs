using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class IceCaster_Skill_Controller : Skill_Controller
{
    Player_IceCaster player_IceCaster;
    public List<GameObject> enemyDetect;
    public GameObject cloestEnemy;
    private float skill_1_Tiemr;
    private float durationTimer;
    private float skill_2_Tiemr;
    private float timer;
    private void Awake()
    {
        player_IceCaster = GetComponent<Player_IceCaster>();
    }
    private void Start()
    {
        durationTimer = DataManager.instance.iceCasterSkill_Data.skill_1_durationTimer;
        skill_1_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_1_CD;
        skill_2_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_2_CD;
        timer = DataManager.instance.iceCasterSkill_Data.skill_1_timer;
    }
    private void Update()
    {
        SectorEnemyDetect();
        skill_1_Tiemr -= Time.deltaTime;
        skill_2_Tiemr -= Time.deltaTime;
        if (skill_1_Tiemr < 0)
        {
            if(player_IceCaster.enemyDetects.Count > 0)
            {
                CreatCircleDamage();
                skill_1_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_1_CD;
            }
        }
        if (skill_2_Tiemr < 0)
        {
            if(enemyDetect.Count > 0)
            {
                durationTimer -= Time.deltaTime;
                if(durationTimer < 0)
                {
                    durationTimer = DataManager.instance.iceCasterSkill_Data.skill_1_durationTimer;
                    skill_2_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_2_CD;
                    return;
                }
                else
                {
                    timer -= Time.deltaTime;
                    if(timer < 0)
                    {
                        SectorDamage();
                        timer = DataManager.instance.iceCasterSkill_Data.skill_1_timer;
                    }
                }
            }
        }

    }
    public void SectorDamage()
    {
        for(int i = 0; i < enemyDetect.Count; i++)
        {
            enemyDetect[i].GetComponent<EnemyStats>().AuthenticTakeDamage(player_IceCaster.stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_1_ExtraAddDamage));
            enemyDetect[i].GetComponent<EnemyBase>().layerOfCold++;
            Debug.Log("1");
        }
    }
    public void SectorEnemyTarget()
    {
        float distance = Mathf.Infinity;
        for(int i = 0; i < enemyDetect.Count; i++)
        {
            if(distance > Vector2.Distance(transform.position, enemyDetect[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, enemyDetect[i].transform.position);
                cloestEnemy = enemyDetect[i];
            }
        }
        if(enemyDetect.Count > 0)
        {
            Vector2 attackDir = (transform.position - cloestEnemy.transform.position).normalized;

            float x = 1 * Mathf.Cos(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);
            float y = 1 * Mathf.Sin(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);

            float cloestEnemyAngle = Mathf.Atan2(attackDir.y, attackDir.x);

            for (int i = 0;i < enemyDetect.Count; i++)
            {
                if (Mathf.Atan2(enemyDetect[i].transform.position.y, enemyDetect[i].transform.position.x) * Mathf.Rad2Deg > cloestEnemyAngle + 30 || Mathf.Atan2(enemyDetect[i].transform.position.y, enemyDetect[i].transform.position.x) < cloestEnemyAngle - 30)
                    enemyDetect.Remove(enemyDetect[i]);
            }
        }
    }
    public void SectorEnemyDetect()
    {
        enemyDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_2_radius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                enemyDetect.Add(hit.gameObject);
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