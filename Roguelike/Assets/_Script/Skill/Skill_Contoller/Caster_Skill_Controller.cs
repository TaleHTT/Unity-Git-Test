using UnityEngine;
using UnityEngine.Pool;

public class Caster_Skill_Controller : MonoBehaviour
{
    private ObjectPool<GameObject> burningRingsPool;
    private ObjectPool<GameObject> meteoritePool;
    [Tooltip("燃烧圈预制体")]
    public GameObject burningRingsPrefab;
    [Tooltip("陨石预制体")]
    public GameObject meteoritePrefab;
    private int numberOfAttack;
    public float explodeDamage;
    public float timer;
    Player_Caster player_Caster;
    private void Awake()
    {
        player_Caster = GetComponent<Player_Caster>();
        timer = DataManager.instance.caster_Skill_Data.CD;
        meteoritePool = new ObjectPool<GameObject>(CreateMeteoritesFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        burningRingsPool = new ObjectPool<GameObject>(CreateburningRingsFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        player_Caster.orbPerfab.GetComponent<Orb_Controller>().burningRingsPool = burningRingsPool;
        player_Caster.orbPerfab.GetComponent<Orb_Controller>().strengthExplosionRadius = DataManager.instance.caster_Skill_Data.skill_1_explodeRadius;
        player_Caster.orbPerfab.GetComponent<Orb_Controller>().strengthExplosionDamage = player_Caster.orbPerfab.GetComponent<Orb_Controller>().damage * (1 + DataManager.instance.caster_Skill_Data.skill_1_extraAddExplodeDamage);
    }
    private void Update()
    {
        StrengthenTheOrb();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            meteoritePool.Get();
            timer = DataManager.instance.caster_Skill_Data.CD;
        }
    }
    private void StrengthenTheOrb()
    {
        if(numberOfAttack >= DataManager.instance.caster_Skill_Data.maxNumberOfAttack)
            player_Caster.orbPerfab.transform.localScale = Vector2.one * 2;
        else
            player_Caster.orbPerfab.transform.localScale = player_Caster.orbPerfab.GetComponent<Orb_Controller>().defaultScale;
    }
    private GameObject CreateMeteoritesFunc()
    {
        var _object = Instantiate(meteoritePrefab, transform.position, Quaternion.identity);
        _object.GetComponent<Meteorite_Conroller>().meteoritePool = meteoritePool;
        return _object;
    }
    private GameObject CreateburningRingsFunc()
    {
        var _object = Instantiate(burningRingsPrefab, player_Caster.orbPerfab.transform.position, Quaternion.identity);
        _object.GetComponent<BurningRings_Controller>().burningRingsPool = burningRingsPool;
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