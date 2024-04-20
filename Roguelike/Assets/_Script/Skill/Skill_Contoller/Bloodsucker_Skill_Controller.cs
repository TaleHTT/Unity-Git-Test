using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bloodsucker_Skill_Controller : MonoBehaviour
{
    ObjectPool<GameObject> parasitismBatAttackPool;
    ObjectPool<GameObject> parasitismBatDefensPool;
    public GameObject parasitismBatDefensPrefab;
    public GameObject parasitismBatAttackPrefab;
    bool isDefens;
    int maxDefensNum = 2;
    public int currentDefenNum;
    public int parasitismBatNum {  get; set; }
    public float duration;
    int maxNum;
    Vector2 size;
    int maxBlood;
    int currentNum;
    float indexTimer;
    int currentBlood;
    float skill_2_Timer;
    public Player_Bloodsucker player_Bloodsucker => GetComponent<Player_Bloodsucker>();
    private void Awake()
    {
        parasitismBatAttackPool = new ObjectPool<GameObject>(CreateParasitismBatAttackFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        parasitismBatDefensPool = new ObjectPool<GameObject>(CreateParasitismBatDefensFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        duration = DataManager.instance.bloodsucker_Skill_Data.skill_2_Duration;
        skill_2_Timer = DataManager.instance.bloodsucker_Skill_Data.skill_2_CD;
        size = new Vector2(DataManager.instance.bloodsucker_Skill_Data.length, DataManager.instance.bloodsucker_Skill_Data.width);
        indexTimer = DataManager.instance.bloodsucker_Skill_Data.indexTimer;
    }
    private void Update()
    {
        indexTimer -= Time.deltaTime;
        skill_2_Timer -= Time.deltaTime;
        if (currentDefenNum <= 0)
            currentDefenNum = maxDefensNum;
        if (currentBlood >= maxBlood)
        {
            if (player_Bloodsucker.position == 0 || player_Bloodsucker.position == 1 || player_Bloodsucker.position == 2)
                RangeDamage();
            else if (player_Bloodsucker.position == 3 || player_Bloodsucker.position == 4 || player_Bloodsucker.position == 5)
            {
                if (currentNum >= maxNum)
                {
                    currentNum = 0;
                    return;
                }
                else
                {
                    StartCoroutine(Damage());
                    currentNum++;
                }
            }
            return;
        }
        else
        {
            if (indexTimer < 0)
            {
                BloodAdd();
                indexTimer = DataManager.instance.bloodsucker_Skill_Data.indexTimer;
            }
        }
        if (skill_2_Timer < 0)
        {
            if(player_Bloodsucker.position == 0 || player_Bloodsucker.position == 1 || player_Bloodsucker.position == 2)
            {
                duration -= Time.deltaTime;
                if(duration > 0)
                {
                    if(parasitismBatNum >= 1)
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
            }else if (player_Bloodsucker.position == 3 || player_Bloodsucker.position == 4 || player_Bloodsucker.position == 5)
            {
                if(isDefens == true)
                    return;
                if(duration > 0)
                {
                    isDefens = true;
                    parasitismBatDefensPool.Get();
                }
            }
        }
    }
    public void BloodAdd()
    {
        player_Bloodsucker.stats.currentHealth -= player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraRemoveHp;
        currentBlood++;
    }
    public void RangeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Bloodsucker.attackRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                hit.GetComponent<EnemyStats>().TakeDamage(player_Bloodsucker.stats.maxHp.GetValue() * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage));
        }
        currentBlood = 0;
    }
    public void RectangleDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll((Vector2)transform.position - size, size, Mathf.Atan2(player_Bloodsucker.closetEnemy.position.y, player_Bloodsucker.closetEnemy.position.x) * Mathf.Rad2Deg);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                hit.GetComponent<EnemyStats>().TakeDamage(player_Bloodsucker.stats.damage.GetValue() * (1 + DataManager.instance.bloodsucker_Skill_Data.skill_1_ExtraAddDamage));
        }
    }
    public IEnumerator Damage()
    {
        yield return new WaitForSeconds(DataManager.instance.bloodsucker_Skill_Data.indexTimerDamage);
        RectangleDamage();
    }
    private GameObject CreateParasitismBatDefensFunc()
    {
        for(int i = -1; i < 2; i += 2)
        {
            var bat = Instantiate(parasitismBatDefensPrefab, new Vector2(transform.position.x + i, transform.position.y), Quaternion.identity);
            bat.GetComponent<ParasitismBatDefens_Controller>().parasitismBatDefensPool = parasitismBatDefensPool;
            bat.GetComponent<ParasitismBatDefens_Controller>().bloodsucker_Skill_Controller = this;
            return bat;
        }
        return null;
    }
    private GameObject CreateParasitismBatAttackFunc()
    {
        var bat = Instantiate(parasitismBatAttackPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<ParasitismBatAttack_Controller>().player_Bloodsucker = player_Bloodsucker;
        bat.GetComponent<ParasitismBatAttack_Controller>().parasitismBatPool = parasitismBatAttackPool;
        return bat;
    }
    private void ActionOnGet(GameObject bat)
    {
        bat.transform.position = transform.position;
        bat.SetActive(true);
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