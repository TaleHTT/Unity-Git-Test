using UnityEngine;
using UnityEngine.Pool;

public class Caster_Skill_Controller : Skill_Controller
{
    private ObjectPool<GameObject> meteoritePool;

    [Tooltip("陨石预制体")]
    public GameObject meteoritePrefab;
    public int numberOfAttack;
    private float timer;
    Player_Caster player_Caster;
    private void Awake()
    {
        player_Caster = GetComponent<Player_Caster>();
        meteoritePool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
    }
    private void Start()
    {
        timer = DataManager.instance.caster_Skill_Data.CD;
        player_Caster.orbPerfab.GetComponent<Orb_Controller>().strengthExplosionDamage = player_Caster.orbPerfab.GetComponent<Orb_Controller>().damage * (1 + DataManager.instance.caster_Skill_Data.skill_1_extraAddExplodeDamage);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if(player_Caster.closetEnemy != null)
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
        _object.GetComponent<Meteorite_Conroller>().meteoritePool = meteoritePool;
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