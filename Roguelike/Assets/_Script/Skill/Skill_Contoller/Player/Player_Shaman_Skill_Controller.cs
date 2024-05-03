using UnityEngine;
using UnityEngine.Pool;

public class Player_Shaman_Skill_Controller : Shaman_Skill_Controller
{
    [HideInInspector] public Player_Shaman player_Shaman;
    protected override void Awake()
    {
        base.Awake();
        player_Shaman = GetComponent<Player_Shaman>();
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
        _object.GetComponent<Player_DeerTotem_Controller>().deerTotemPool = deerTotemPool;
        _object.GetComponent<Player_DeerTotem_Controller>().Hp = player_Shaman.stats.maxHp.GetValue() * DataManager.instance.shaman_Skill_Data.skill_1_AddHp;
        _object.GetComponent<Player_DeerTotem_Controller>().treat = player_Shaman.stats.maxHp.GetValue() * DataManager.instance.shaman_Skill_Data.skill_1_ExtraAddTreatHp + DataManager.instance.shaman_Skill_Data.healBaseValue;
        _object.GetComponent<Player_DeerTotem_Controller>().damage = player_Shaman.stats.damage.GetValue() * DataManager.instance.shaman_Skill_Data.extraAddDamage + DataManager.instance.shaman_Skill_Data.damageBaseValue;
        return _object;
    }
    private GameObject CreateRangeAddMoveSpeedFunc()
    {
        var _object = Instantiate(birdTotemPrefab, transform.position, Quaternion.identity);
        _object.GetComponent<Player_BirdTotem_Controller>().birdTotemPool = birdTotemPool;
        _object.GetComponent<Player_BirdTotem_Controller>().Hp = player_Shaman.stats.maxHp.GetValue() * DataManager.instance.shaman_Skill_Data.skill_2_AddHp;
        _object.GetComponent<Player_BirdTotem_Controller>().damage = player_Shaman.stats.damage.GetValue() * DataManager.instance.shaman_Skill_Data.extraAddDamage + DataManager.instance.shaman_Skill_Data.damageBaseValue;
        return _object;
    }
}