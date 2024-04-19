using UnityEngine;
using UnityEngine.Pool;

public class Archer_Skill_Controller : MonoBehaviour
{
    private int amount_Hound;
    private float skill_2_timer;
    public bool isHave_X_Equipment;
    private int numberOfSpawnsHounds;
    private Vector2 attackDir;
    private int arrowNum;
    public GameObject multipleArrow;
    private int ArrowNum
    {
        get
        {
            return arrowNum;
        }
        set
        {
            arrowNum = player_Archer.stats.level + 3;
            if(arrowNum > 6)
                arrowNum = 6;
        }
    }
    private float angle;
    private float Angle
    {
        get
        {
            return angle;
        }
        set
        {
            angle = 60 / ArrowNum;
        }
    }
    private ObjectPool<GameObject> houndPool;
    private ObjectPool<GameObject> multipleArrowPool;
    [SerializeField] private GameObject Summon_Hound_Prefab;

    Player_Archer player_Archer;
    private void Awake()
    {
        attackDir = (transform.position - player_Archer.closetEnemy.transform.position).normalized;
        amount_Hound = player_Archer.stats.level - 1;
        player_Archer = GetComponent<Player_Archer>();
        skill_2_timer = DataManager.instance.archer_Skill_Data.skill_2_CD;
        houndPool = new ObjectPool<GameObject>(CreateHoundFunc, HoundPoolActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        multipleArrowPool = new ObjectPool<GameObject>(CreatMultipleArrow, MultipleArrowActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        Summon_Hound_Prefab.GetComponent<Summons_Base>().maxHp = player_Archer.stats.maxHp.GetValue() * (0.5f + (float)player_Archer.stats.level / 10);
        Summon_Hound_Prefab.GetComponent<Summons_Base>().damage = player_Archer.stats.damage.GetValue() * (0.5f + (float)player_Archer.stats.level / 10);
    }
    private void Update()
    {
        attackDir = (transform.position - player_Archer.closetEnemy.transform.position).normalized;
        if(numberOfSpawnsHounds < amount_Hound)
            skill_2_timer -= Time.deltaTime;
        if (skill_2_timer <= 0 && amount_Hound > 0 && numberOfSpawnsHounds <= amount_Hound)
        {
            houndPool.Get();
            numberOfSpawnsHounds++;
            skill_2_timer = DataManager.instance.archer_Skill_Data.skill_2_CD;
        }
    }
    private GameObject CreatMultipleArrow()
    {
        for(float i = 0; i < 60; i += Angle)
        {
            float x = 1 * Mathf.Cos((i + 60 - Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg) * Mathf.Deg2Rad);
            float y = 1 * Mathf.Sin((i + 60 - Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg) * Mathf.Deg2Rad);

            var objects = Instantiate(multipleArrow, transform.position, Quaternion.identity);
            objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
            objects.GetComponent<MultipleArrow_Controller>().moveDir = objects.transform.position - transform.position;
            objects.GetComponent<MultipleArrow_Controller>().multipleArrowPool = multipleArrowPool;
            objects.GetComponent<MultipleArrow_Controller>().damage = player_Archer.stats.damage.GetValue() * (1 + DataManager.instance.archer_Skill_Data.extraAddDamage);
            return objects;
        }
        return null;
    }
    private GameObject CreateHoundFunc()
    {
        var hound = Instantiate(Summon_Hound_Prefab, transform.position, Quaternion.identity);
        hound.GetComponent<Summons_Base>().houndPool = houndPool;
        return hound;
    }
    private void MultipleArrowActionOnGet(GameObject objects)
    {
        for (float i = 0f; i < 60; i += Angle)
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
