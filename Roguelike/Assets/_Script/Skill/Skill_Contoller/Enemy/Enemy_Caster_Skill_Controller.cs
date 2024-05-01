using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Caster_Skill_Controller : Caster_Skill_Controller
{
    private Enemy_Caster enemy_Caster;
    protected override void Awake()
    {
        base.Awake();
        enemy_Caster = GetComponent<Enemy_Caster>();
        meteoritePool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    protected override void Start()
    {
        base.Start();
        enemy_Caster.OrbPerfab.GetComponent<Enemy_Orb_Controller>().strengthExplosionDamage = enemy_Caster.OrbPerfab.GetComponent<Orb_Controller>().damage * (1 + DataManager.instance.caster_Skill_Data.skill_1_extraAddExplodeDamage);
    }
    protected override void Update()
    {
        base.Update();
        if (timer <= 0)
        {
            if (enemy_Caster.cloestTarget != null)
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
        _object.GetComponent<Enemy_Meteorite_Conroller>().meteoritePool = meteoritePool;
        return _object;
    }
}