using UnityEngine;
using UnityEngine.Pool;

public class Caster_Skill_Controller : Skill_Controller
{
    [Tooltip("陨石预制体")]
    public GameObject meteoritePrefab;
    public ObjectPool<GameObject> meteoritePool;

    [HideInInspector] public float timer;
    [HideInInspector] public int numberOfAttack;
    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        timer = DataManager.instance.caster_Skill_Data.CD;
    }
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
    }
    public void ActionOnGet(GameObject _object)
    {
        _object.transform.position = transform.position;
        _object.SetActive(true);
    }
    public void ActionOnRelease(GameObject _object)
    {
        _object.SetActive(false);
    }
    public void ActionOnDestory(GameObject _object)
    {
        Destroy(_object);
    }
}