using UnityEngine;
using UnityEngine.Pool;

public class Archer_Skill_Controller : Skill_Controller
{
    [HideInInspector] public int amount_Hound;
    [HideInInspector] public Vector2 attackDir;
    [HideInInspector] public float skill_1_timer;
    [HideInInspector] public float skill_2_timer;
    private int numberOfSpawnsHounds;
    public GameObject multipleArrow;
    public GameObject Summon_Hound_Prefab;

    [HideInInspector] public int angle;
    [HideInInspector] public int arrowNum;
    [HideInInspector] public int eachAngle;
    [HideInInspector] public ObjectPool<GameObject> houndPool;
    [HideInInspector] public ObjectPool<GameObject> multipleArrowPool;


    protected virtual void Awake()
    {
        houndPool = new ObjectPool<GameObject>(CreateHoundFunc, HoundPoolActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    } 
    protected virtual void Start()
    {
        skill_1_timer = DataManager.instance.archer_Skill_Data.skill_1_CD;
        skill_2_timer = DataManager.instance.archer_Skill_Data.skill_2_CD;
    }
    protected virtual void Update()
    {
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
    public GameObject CreateHoundFunc()
    {
        var hound = Instantiate(Summon_Hound_Prefab, transform.position, Quaternion.identity);
        hound.GetComponent<Summons_Base>().houndPool = houndPool;
        return hound;
    }
    public void MultipleArrowActionOnGet(GameObject objects)
    {
        float x = 1 * Mathf.Cos((eachAngle + (Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 30)) * Mathf.Deg2Rad);
        float y = 1 * Mathf.Sin((eachAngle + (Mathf.Atan2(attackDir.y, attackDir.x) * Mathf.Rad2Deg - 30)) * Mathf.Deg2Rad);

        objects.transform.position = new Vector2(x + transform.position.x, y + transform.position.y);
        objects.SetActive(true);
    }
    public void HoundPoolActionOnGet(GameObject objects)
    {
        objects.GetComponent<Summons_Base>().isDead = false;
        objects.transform.position = transform.position;
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
