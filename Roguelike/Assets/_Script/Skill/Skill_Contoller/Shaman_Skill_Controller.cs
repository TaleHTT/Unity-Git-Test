using UnityEngine;
using UnityEngine.Pool;

public class Shaman_Skill_Controller : Skill_Controller
{
    private float skill_1_Timer;
    private float skill_2_Timer;
    public GameObject deerTotemPrefab;
    public GameObject birdTotemPrefab;
    private Player_Shaman player_Shaman;
    private ObjectPool<GameObject> deerTotemPool;
    private ObjectPool<GameObject> birdTotemPool;
    private void Awake()
    {
        player_Shaman = GetComponent<Player_Shaman>();
        deerTotemPool = new ObjectPool<GameObject>(CreateRangeTreatFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        birdTotemPool = new ObjectPool<GameObject>(CreateRangeAddMoveSpeedFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    private void Start()
    {
        skill_1_Timer = DataManager.instance.shaman_Skill_Data.skill_1_CD;
        skill_2_Timer = DataManager.instance.shaman_Skill_Data.skill_2_CD;
    }
    private void Update()
    {
        skill_1_Timer -= Time.deltaTime;
        skill_2_Timer -= Time.deltaTime;
        if (skill_1_Timer <= 0)
        {
            deerTotemPool.Get();
            skill_1_Timer = DataManager.instance.shaman_Skill_Data.skill_1_CD;
        }
        if (skill_2_Timer <= 0)
        {
            birdTotemPool.Get();
            skill_2_Timer = DataManager.instance.shaman_Skill_Data.skill_2_CD;
        }
    }
    private GameObject CreateRangeTreatFunc()
    {
        var _object = Instantiate(deerTotemPrefab, transform.position, Quaternion.identity);
        _object.GetComponent<DeerTotem_Controller>().deerTotemPool = deerTotemPool;
        _object.GetComponent<DeerTotem_Controller>().Hp = player_Shaman.stats.maxHp.GetValue() * DataManager.instance.shaman_Skill_Data.skill_1_AddHp;
        _object.GetComponent<DeerTotem_Controller>().treat = player_Shaman.stats.maxHp.GetValue() * (1 + DataManager.instance.shaman_Skill_Data.skill_1_ExtraAddTreatHp);
        _object.GetComponent<DeerTotem_Controller>().damage = player_Shaman.stats.damage.GetValue() * (1 + DataManager.instance.shaman_Skill_Data.extraAddDamage);
        return _object;
    }
    private GameObject CreateRangeAddMoveSpeedFunc()
    {
        var _object = Instantiate(birdTotemPrefab, transform.position, Quaternion.identity);
        _object.GetComponent<BirdTotem_Controller>().birdTotemPool = birdTotemPool;
        _object.GetComponent<BirdTotem_Controller>().Hp = player_Shaman.stats.maxHp.GetValue() * DataManager.instance.shaman_Skill_Data.skill_2_AddHp;
        _object.GetComponent<BirdTotem_Controller>().damage = player_Shaman.stats.damage.GetValue() * (1 + DataManager.instance.shaman_Skill_Data.extraAddDamage);
        return _object;
    }
    private void ActionOnGet(GameObject _object)
    {
        _object.transform.position = transform.position;
        _object.SetActive(true);
    }
    private void ActionOnRelease(GameObject _object)
    {
        _object.SetActive(false);
    }
    private void ActionOnDestory(GameObject _object)
    {
        Destroy(_object);
    }
}