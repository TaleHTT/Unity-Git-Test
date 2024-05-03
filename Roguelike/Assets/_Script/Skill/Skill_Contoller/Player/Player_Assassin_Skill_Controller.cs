using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player_Assassin_Skill_Controller : Assassin_Skill_Controller
{
    public GameObject daggerPrefab;

    [HideInInspector] public int angle;
    [HideInInspector] public int num_KillEnemy;
    [HideInInspector] public int eachAngle;
    [HideInInspector] public int bulletNum;
    [HideInInspector] public Player_Assassin player_Assassin;
    [HideInInspector] public ObjectPool<GameObject> daggerPool;

    protected override void Awake()
    {
        base.Awake();
        player_Assassin = GetComponent<Player_Assassin>();
        daggerPool = new ObjectPool<GameObject>(CreateDaggerFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        value = player_Assassin.stats.attackSpeed.GetValue();
        baseValue = player_Assassin.stats.attackSpeed.baseValue;
        bulletNum = 8 * player_Assassin.stats.level;
        if (bulletNum >= 24)
            bulletNum = 24;
        angle = 360 / bulletNum;
    }
    protected override void Update()
    {
        base.Update();
        HuntingDectect();
        if (SkillManger.instance.assassin_Skill.isHave_X_Equipment)
            if (num_KillEnemy == 3)
                for (int i = 0; i < bulletNum; i++)
                {
                    eachAngle = angle * i;
                    daggerPool.Get();
                    num_KillEnemy = 0;
                }

        if (huntTarget.Count > 0)
            player_Assassin.stats.attackSpeed.baseValue = (1 + DataManager.instance.assassin_Skill_Data.extraAddAttackSpeed) * value;
        else
        {
            player_Assassin.stats.attackSpeed.baseValue = baseValue;
        }
    }
    public void HuntingDectect()
    {
        huntTarget = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                if (hit.GetComponent<EnemyBase>().isHunting == true)
                    huntTarget.Add(hit.gameObject);
            }
        }
    }
    private GameObject CreateDaggerFunc()
    {
        float x = 1 * Mathf.Cos(eachAngle * Mathf.Deg2Rad);
        float y = 1 * Mathf.Sin(eachAngle * Mathf.Deg2Rad);

        var objects = Instantiate(daggerPrefab, transform.position, Quaternion.identity);
        objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
        objects.GetComponent<Dagger_Controller>().moveDir = objects.transform.position - transform.position;
        objects.GetComponent<Dagger_Controller>().daggerPool = daggerPool;
        objects.GetComponent<Dagger_Controller>().player_Assassin_Skill_Controller = this;
        objects.GetComponent<Dagger_Controller>().damage = player_Assassin.stats.damage.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddDamage + DataManager.instance.assassin_Skill_Data.damageBaseValue;
        objects.GetComponent<Dagger_Controller>().treat = player_Assassin.stats.damage.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddHp + DataManager.instance.assassin_Skill_Data.healBaseValue;
        return objects;
    }
    private void ActionOnGet(GameObject objects)
    {
        float x = 1 * Mathf.Cos(eachAngle * Mathf.Deg2Rad);
        float y = 1 * Mathf.Sin(eachAngle * Mathf.Deg2Rad);

        objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
        objects.SetActive(true);
    }
    public void ActionOnRelease(GameObject objects)
    {
        objects.SetActive(false);
    }
    public void ActionOnDestory(GameObject objects)
    {
        Destroy(objects);
    }
}