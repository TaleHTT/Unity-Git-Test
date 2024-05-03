using UnityEngine;
using UnityEngine.Pool;

public class Player_Caster_Skill_Controller : Caster_Skill_Controller
{
    private Player_Caster player_Caster;
    protected override void Awake()
    {
        base.Awake();
        player_Caster = GetComponent<Player_Caster>();
        meteoritePool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        player_Caster.orbPerfab.GetComponent<Player_Orb_Controller>().strengthExplosionDamage = player_Caster.stats.damage.GetValue() * DataManager.instance.caster_Skill_Data.skill_1_extraAddExplodeDamage + DataManager.instance.caster_Skill_Data.explodeDamageBaseValue;
    }
    protected override void Update()
    {
        base.Update();
        if (timer <= 0)
        {
            if (player_Caster.closetEnemy != null)
            {
                meteoritePool.Get();
                timer = DataManager.instance.caster_Skill_Data.CD;
            }
            else
            {
                return;
            }
        }
    }
    private GameObject CreateFunc()
    {
        var _object = Instantiate(meteoritePrefab, transform.position, Quaternion.identity);
        _object.GetComponent<Player_Meteorite_Conroller>().meteoritePool = meteoritePool;
        return _object;
    }
}