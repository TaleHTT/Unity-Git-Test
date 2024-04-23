using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bloodsucker_Skill_Controller : MonoBehaviour
{
    ObjectPool<GameObject> parasitismBatAttackPool;
    ObjectPool<GameObject> parasitismBatDefensPool;
    ObjectPool<GameObject> rectanglePool;
    public GameObject parasitismBatDefensPrefab;
    public GameObject parasitismBatAttackPrefab;
    public GameObject rectanglePrefab;
    int maxDefensNum = 2;
    public int currentDefenNum;
    public int parasitismBatNum { get; set; }
    public float duration;
    const int maxBlood = 5;
    public int currentNum;
    float indexTimer;
    public int currentBlood;
    float skill_2_Timer;
    public float timer = 1;
    Vector2 attackDir;
    List<GameObject> skillDetect;
    int num = 1;
    public Player_Bloodsucker player_Bloodsucker => GetComponent<Player_Bloodsucker>();
    private void Awake()
    {
        rectanglePool = new ObjectPool<GameObject>(CreatRectangleFunc, ActionRectangleOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        parasitismBatAttackPool = new ObjectPool<GameObject>(CreateParasitismBatAttackFunc, ActionParasitismBatAttackOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        parasitismBatDefensPool = new ObjectPool<GameObject>(CreateParasitismBatDefensFunc, ActionParasitismBatAttackOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    private void Start()
    {
        duration = DataManager.instance.bloodsucker_Skill_Data.skill_2_Duration;
        skill_2_Timer = DataManager.instance.bloodsucker_Skill_Data.skill_2_CD;
        indexTimer = DataManager.instance.bloodsucker_Skill_Data.indexTimer;
    }
    private void Update()
    {
        if (player_Bloodsucker.closetEnemy != null)
        {
            attackDir = (player_Bloodsucker.closetEnemy.transform.position - transform.position).normalized;
            SkillDetect();
        }
        skill_2_Timer -= Time.deltaTime;
        if (currentDefenNum <= 0)
            currentDefenNum = maxDefensNum;
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
            if (player_Bloodsucker.position == 0 || player_Bloodsucker.position == 1 || player_Bloodsucker.position == 2)
            {
                duration -= Time.deltaTime;
                if (duration > 0)
                {
                    if (parasitismBatNum >= 1)
                        return;
                    else
                    {
                        parasitismBatAttackPool.Get();
                        parasitismBatNum++;
                    }
                }
                else
                {
                    skill_2_Timer = DataManager.instance.bloodsucker_Skill_Data.skill_2_CD;
                    duration = DataManager.instance.bloodsucker_Skill_Data.skill_2_Duration;
                }
            }
            else if (player_Bloodsucker.position == 3 || player_Bloodsucker.position == 4 || player_Bloodsucker.position == 5)
            {
                for(int i = 0; i < 2; i++)
                {
                    parasitismBatDefensPool.Get();
                    num *= -1;
                }
            }
        }
    }
    public void SkillDetect()
    {
        skillDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.bloodsucker_Skill_Data.length, player_Bloodsucker.whatIsEnemy);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                skillDetect.Add(hit.gameObject);
        }
    }
    public void BloodAdd()
    {
        player_Bloodsucker.stats.currentHealth -= player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraRemoveHp;
        currentBlood++;
    }
    public void RangeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Bloodsucker.attackRadius, player_Bloodsucker.whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
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
        rect.GetComponent<RectangleDamage_Controller>().rectanglePool = rectanglePool;
        rect.GetComponent<RectangleDamage_Controller>().damage = player_Bloodsucker.batPrefab.GetComponent<Bat_Controller>().damage * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage);
        return rect;
    }
    private GameObject CreateParasitismBatDefensFunc()
    {
        var bat = Instantiate(parasitismBatDefensPrefab, new Vector2(transform.position.x + num, transform.position.y), Quaternion.identity);
        bat.GetComponent<ParasitismBatDefens_Controller>().parasitismBatDefensPool = parasitismBatDefensPool;
        bat.GetComponent<ParasitismBatDefens_Controller>().bloodsucker_Skill_Controller = this;
        return bat;
    }
    private GameObject CreateParasitismBatAttackFunc()
    {
        var bat = Instantiate(parasitismBatAttackPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<ParasitismBatAttack_Controller>().parasitismBatPool = parasitismBatAttackPool;
        bat.GetComponent<ParasitismBatAttack_Controller>().bloodsucker_Skill_Controller = this;
        return bat;
    }
    private void ActionRectangleOnGet(GameObject rectangle)
    {
        rectangle.transform.position = new Vector2((transform.position.x + DataManager.instance.bloodsucker_Skill_Data.length / 2), transform.position.y);
        rectangle.SetActive(true);
    }
    private void ActionParasitismBatAttackOnGet(GameObject bat)
    {
        bat.transform.position = transform.position;
        bat.SetActive(true);
    }
    private void ActionParasitismBatDefensOnGet(GameObject bat)
    {
        for(int i = 0; i < 2; i++)
        {
            bat.transform.position = new Vector2(transform.position.x + num, transform.position.y);
            bat.SetActive(true);
            num *= -1;
        }
    }
    private void ActionOnRelease(GameObject bat)
    {
        bat.SetActive(false);
    }
    private void ActionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}