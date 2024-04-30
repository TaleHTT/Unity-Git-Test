using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Shaman_Skill_Controller : Shaman_Skill_Controller
{
    [HideInInspector] public Enemy_Shaman enemy_Shaman;
    protected override void Awake()
    {
        base.Awake();
        enemy_Shaman = GetComponent<Enemy_Shaman>();
        deerTotemPool = new ObjectPool<GameObject>(CreateRangeTreatFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        birdTotemPool = new ObjectPool<GameObject>(CreateRangeAddMoveSpeedFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    private GameObject CreateRangeTreatFunc()
    {
        var _object = Instantiate(deerTotemPrefab, transform.position, Quaternion.identity);
        _object.GetComponent<Enemy_DeerTotem_Controller>().deerTotemPool = deerTotemPool;
        _object.GetComponent<Enemy_DeerTotem_Controller>().Hp = enemy_Shaman.stats.maxHp.GetValue() * DataManager.instance.shaman_Skill_Data.skill_1_AddHp;
        _object.GetComponent<Enemy_DeerTotem_Controller>().treat = enemy_Shaman.stats.maxHp.GetValue() * (1 + DataManager.instance.shaman_Skill_Data.skill_1_ExtraAddTreatHp);
        return _object;
    }
    private GameObject CreateRangeAddMoveSpeedFunc()
    {
        var _object = Instantiate(birdTotemPrefab, transform.position, Quaternion.identity);
        _object.GetComponent<Enemy_BirdTotem_Controller>().birdTotemPool = birdTotemPool;
        _object.GetComponent<Enemy_BirdTotem_Controller>().Hp = enemy_Shaman.stats.maxHp.GetValue() * DataManager.instance.shaman_Skill_Data.skill_2_AddHp;
        return _object;
    }
}