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
    public int numberOfAttack;
    public int maxNumberOfAttack;
    public float explodeDamage;
    public float explodeRadius;
    [SerializeField][Range(0, 1)] private float add; 
    Player_Caster player_Caster;
    private void Awake()
    {
        player_Caster = GetComponent<Player_Caster>();
        meteoritePool = new ObjectPool<GameObject>(CreatemeteoritesFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        burningRingsPool = new ObjectPool<GameObject>(CreateburningRingsFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        player_Caster.orbPerfab.GetComponent<Orb_Controller>().burningRingsPool = burningRingsPool;
        player_Caster.orbPerfab.GetComponent<Orb_Controller>().strengthExplosionRadius = explodeRadius;
        player_Caster.orbPerfab.GetComponent<Orb_Controller>().strengthExplosionDamage = player_Caster.orbPerfab.GetComponent<Orb_Controller>().damage * (1 + add);
    }
    private void Update()
    {
        StrengthenTheOrb();
        if (SkillManger.instance.caster_Skill.coolDown <= 0)
            meteoritePool.Get();
    }
    private void StrengthenTheOrb()
    {
        if(numberOfAttack >= maxNumberOfAttack)
        {
            player_Caster.orbPerfab.transform.localScale = Vector2.one * 2;
            player_Caster.orbPerfab.GetComponent<Orb_Controller>().cd.radius = player_Caster.orbPerfab.GetComponent<Orb_Controller>().cd.radius * 2;
        }
        else
        {
            player_Caster.orbPerfab.transform.localScale = player_Caster.orbPerfab.GetComponent<Orb_Controller>().defaultScale;
            player_Caster.orbPerfab.GetComponent<Orb_Controller>().cd.radius = player_Caster.orbPerfab.GetComponent<Orb_Controller>().cdDefaultRadius;
        }
    }
    private GameObject CreatemeteoritesFunc()
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