using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Archer_Skill_Controller : Archer_Skill_Controller
{
    private Enemy_Archer enemy_Archer;

    protected override void Awake()
    {
        base.Awake();
        enemy_Archer = GetComponent<Enemy_Archer>();
        multipleArrowPool = new ObjectPool<GameObject>(CreatMultipleArrow, MultipleArrowActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        Summon_Hound_Prefab.GetComponent<Summons_Base>().damage = enemy_Archer.stats.damage.GetValue() * (0.5f + (float)enemy_Archer.stats.level / 10);
        Summon_Hound_Prefab.GetComponent<Summons_Base>().maxHp = enemy_Archer.stats.maxHp.GetValue() * (0.5f + enemy_Archer.arrowPerfab.GetComponent<Arrow_Controller>().damage / 10);
        arrowNum = enemy_Archer.stats.level + 3;
        if (arrowNum > 6)
            arrowNum = 6;
        angle = 60 / (arrowNum - 1);
        amount_Hound = enemy_Archer.stats.level - 1;
    }
    protected override void Update()
    {
        base.Update();
        if (enemy_Archer.cloestTarget != null)
            attackDir = (enemy_Archer.cloestTarget.transform.position - transform.position).normalized;
        skill_1_timer -= Time.deltaTime;
        Debug.Log(skill_1_timer);
        if (skill_1_timer < 0)
        {
            if (enemy_Archer.cloestTarget != null)
            {
                for (int i = 0; i < arrowNum; i++)
                {
                    eachAngle = angle * i;
                    multipleArrowPool.Get();
                }
                skill_1_timer = DataManager.instance.archer_Skill_Data.skill_1_CD;
            }
        }
    }
    private GameObject CreatMultipleArrow()
    {
        float x = 1 * Mathf.Cos((eachAngle + (Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 30)) * Mathf.Deg2Rad);
        float y = 1 * Mathf.Sin((eachAngle + (Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 30)) * Mathf.Deg2Rad);

        var objects = Instantiate(multipleArrow);
        objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
        objects.GetComponent<Enemy_MultipleArrow_Controller>().enemy_Archer = enemy_Archer;
        objects.GetComponent<Enemy_MultipleArrow_Controller>().multipleArrowPool = multipleArrowPool;
        objects.GetComponent<Enemy_MultipleArrow_Controller>().damage = enemy_Archer.arrowPerfab.GetComponent<Arrow_Controller>().damage * (1 + DataManager.instance.archer_Skill_Data.extraAddDamage);
        return objects;
    }
}