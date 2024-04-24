using UnityEngine;
using UnityEngine.Pool;

public class Archer_Skill_Controller : Skill_Controller
{
    private int amount_Hound;
    private Vector2 attackDir;
    private float skill_1_timer;
    private float skill_2_timer;
    public bool isHave_X_Equipment;
    public GameObject multipleArrow;
    private int numberOfSpawnsHounds;

    private int angle;
    private int arrowNum;
    private int eachAngle;
    private ObjectPool<GameObject> houndPool;
    private ObjectPool<GameObject> multipleArrowPool;
    [SerializeField] private GameObject Summon_Hound_Prefab;

    [SerializeField] Player_Archer player_Archer;
    private void Awake()
    {
        player_Archer = GetComponent<Player_Archer>();
        houndPool = new ObjectPool<GameObject>(CreateHoundFunc, HoundPoolActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        multipleArrowPool = new ObjectPool<GameObject>(CreatMultipleArrow, MultipleArrowActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    private void Start()
    {
        Summon_Hound_Prefab.GetComponent<Summons_Base>().damage = player_Archer.stats.damage.GetValue() * (0.5f + (float)player_Archer.stats.level / 10);
        Summon_Hound_Prefab.GetComponent<Summons_Base>().maxHp = player_Archer.stats.maxHp.GetValue() * (0.5f + player_Archer.arrowPerfab.GetComponent<Arrow_Controller>().damage / 10);
        arrowNum = player_Archer.stats.level + 3;
        if(arrowNum > 6)
            arrowNum = 6;
        angle = 60 / (arrowNum - 1);
        amount_Hound = player_Archer.stats.level - 1;
        skill_1_timer = DataManager.instance.archer_Skill_Data.skill_1_CD;
        skill_2_timer = DataManager.instance.archer_Skill_Data.skill_2_CD;
    }
    private void Update()
    {
        if(player_Archer.closetEnemy != null)
            attackDir = (player_Archer.closetEnemy.transform.position - transform.position).normalized;
        skill_1_timer -= Time.deltaTime;
        if(skill_1_timer < 0)
        {
            if(player_Archer.closetEnemy != null)
            {
                for(int i = 0; i < arrowNum; i++)
                {
                    eachAngle = angle * i;
                    multipleArrowPool.Get();
                }
                skill_1_timer = DataManager.instance.archer_Skill_Data.skill_1_CD;
            }
            skill_1_timer = DataManager.instance.archer_Skill_Data.skill_1_CD;
        }
        if (skill_1_timer <= 0)
        {
            multipleArrowPool.Get();
            skill_1_timer = DataManager.instance.archer_Skill_Data.skill_1_CD;
        }
        if (numberOfSpawnsHounds < amount_Hound)
        {
            skill_2_timer -= Time.deltaTime;
            if (skill_2_timer <= 0 && amount_Hound > 0)
            {
                houndPool.Get();
                numberOfSpawnsHounds++;
                skill_2_timer = DataManager.instance.archer_Skill_Data.skill_2_CD;
            }
        }
    }
    private GameObject CreatMultipleArrow()
    {
            float x = 1 * Mathf.Cos((eachAngle + (Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 30)) * Mathf.Deg2Rad);
            float y = 1 * Mathf.Sin((eachAngle + (Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 30)) * Mathf.Deg2Rad);

            var objects = Instantiate(multipleArrow);
            objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
            objects.GetComponent<MultipleArrow_Controller>().moveDir = objects.transform.position - transform.position;
            objects.GetComponent<MultipleArrow_Controller>().multipleArrowPool = multipleArrowPool;
            objects.GetComponent<MultipleArrow_Controller>().damage = player_Archer.arrowPerfab.GetComponent<Arrow_Controller>().damage * (1 + DataManager.instance.archer_Skill_Data.extraAddDamage);
            return objects;
    }
    private GameObject CreateHoundFunc()
    {
        var hound = Instantiate(Summon_Hound_Prefab, transform.position, Quaternion.identity);
        hound.GetComponent<Summons_Base>().houndPool = houndPool;
        return hound;
    }
    private void MultipleArrowActionOnGet(GameObject objects)
    {
        for (float i = 0f; i < 60; i += angle)
        {
            float x = 1 * Mathf.Cos((i + 60 - Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg) * Mathf.Deg2Rad);
            float y = 1 * Mathf.Sin((i + 60 - Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg) * Mathf.Deg2Rad);

            objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
            objects.SetActive(true);
        }
    }
    private void HoundPoolActionOnGet(GameObject objects)
    {
        objects.GetComponent<Summons_Base>().isDead = false;
        objects.transform.position = transform.position;
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
}
