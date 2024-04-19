using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class IceCaster_Skill_Controller : MonoBehaviour
{
    public GameObject sectorPrefab;
    Player_IceCaster player_IceCaster;
    private Vector2 attackDir;
    private List<GameObject> enemyDetect;
    private GameObject cloestEnemy;
    private float skill_1_Tiemr;
    private float durationTimer;
    private float skill_2_Tiemr;
    private void Awake()
    {
        durationTimer = DataManager.instance.iceCasterSkill_Data.durationTimer;
        attackDir = (transform.position - player_IceCaster.closetEnemy.transform.position).normalized;
        skill_1_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_1_CD;
        skill_2_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_2_CD;
        player_IceCaster = GetComponent<Player_IceCaster>();
    }
    private void Update()
    {
        skill_1_Tiemr -= Time.deltaTime;
        skill_2_Tiemr -= Time.deltaTime;
        if (skill_1_Tiemr < 0)
        {
            CreatCircleDamage();
            skill_1_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_1_CD;
        }
        if (skill_2_Tiemr < 0)
        {
            if(durationTimer < 0)
            {
                durationTimer = DataManager.instance.iceCasterSkill_Data.durationTimer;
                skill_2_Tiemr = DataManager.instance.iceCasterSkill_Data.skill_2_CD;
                return;
            }
            else
            {
                durationTimer -= Time.deltaTime;
                StartCoroutine(CreatSector());
            }
        }

    }
    public void SectorDamage()
    {
        for(int i = 0; i < enemyDetect.Count; i++)
        {
            enemyDetect[i].GetComponent<EnemyStats>().AuthenticTakeDamage(player_IceCaster.stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_1_ExtraAddDamage));
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
        Vector2 attackDir = (transform.position - cloestEnemy.transform.position).normalized;

        float x = 1 * Mathf.Cos(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);
        float y = 1 * Mathf.Sin(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);

        float cloestEnemyAngle = Mathf.Atan2(attackDir.y, attackDir.x);

        for (int i = 0;i < enemyDetect.Count; i++)
        {
            if (Mathf.Atan2(enemyDetect[i].transform.position.y, enemyDetect[i].transform.position.x) > cloestEnemyAngle + 30 || Mathf.Atan2(enemyDetect[i].transform.position.y, enemyDetect[i].transform.position.x) < cloestEnemyAngle - 30)
                enemyDetect.Remove(enemyDetect[i]);
        }
    }
    public void SectorEnemyDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_1_radius);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                enemyDetect.Add(hit.gameObject);
        }
    }
    public void CreatCircleDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_2_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                hit.GetComponent<EnemyStats>().AuthenticTakeDamage(player_IceCaster.stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_2_ExtraAddDamage));
        }
    }
    private void ActionOnGet(GameObject objects)
    {
        float x = 1 * Mathf.Cos(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);
        float y = 1 * Mathf.Sin(Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg * Mathf.Deg2Rad);

        objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
        objects.SetActive(true);
    }
    private void ActionOnRelease(GameObject objects)
    {
        objects.SetActive(false);
    }
    private void ActionOnDestory(GameObject objects)
    {
        Destroy(objects);
    }
    public IEnumerator CreatSector()
    {
        yield return new WaitForSeconds(DataManager.instance.iceCasterSkill_Data.attack_Speed);

        SectorDamage();
    }
}