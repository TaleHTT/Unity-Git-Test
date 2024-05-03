using UnityEngine;
using UnityEngine.Pool;

public class Player_Archer_Skill_Controller : Archer_Skill_Controller
{
    private Player_Archer player_Archer;

    protected override void Awake()
    {
        base.Awake();
        player_Archer = GetComponent<Player_Archer>();
        multipleArrowPool = new ObjectPool<GameObject>(CreatMultipleArrow, MultipleArrowActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        Summon_Hound_Prefab.GetComponent<Summons_Base>().damage = player_Archer.stats.damage.GetValue() * (0.5f + (float)player_Archer.stats.level / 10);
        Summon_Hound_Prefab.GetComponent<Summons_Base>().maxHp = player_Archer.stats.maxHp.GetValue() * (0.5f + player_Archer.arrowPerfab.GetComponent<Arrow_Controller>().damage / 10);
        arrowNum = player_Archer.stats.level + 3;
        if (arrowNum > 6)
            arrowNum = 6;
        angle = 60 / (arrowNum - 1);
        amount_Hound = player_Archer.stats.level - 1;
    }
    protected override void Update()
    {
        base.Update();
        if (player_Archer.closetEnemy != null)
            attackDir = (player_Archer.closetEnemy.transform.position - transform.position).normalized;
        skill_1_timer -= Time.deltaTime;
        Debug.Log(skill_1_timer);
        if (skill_1_timer < 0)
        {
            if (player_Archer.closetEnemy != null)
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

        var objects = Instantiate(multipleArrow, this.transform);
        objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
        objects.GetComponent<Player_MultipleArrow_Controller>().player_Archer = player_Archer;
        objects.GetComponent<Player_MultipleArrow_Controller>().multipleArrowPool = multipleArrowPool;
        objects.GetComponent<Player_MultipleArrow_Controller>().damage = player_Archer.stats.damage.GetValue() * DataManager.instance.archer_Skill_Data.extraAddDamage + DataManager.instance.archer_Skill_Data.damageBaseValue;
        return objects;
    }
}