using UnityEngine;
using UnityEngine.Pool;

public class Shaman_Skill_Controller : Skill_Controller
{
    [HideInInspector] public float skill_1_Timer;
    [HideInInspector] public float skill_2_Timer;
   
    public GameObject deerTotemPrefab;
    public GameObject birdTotemPrefab;
    public ObjectPool<GameObject> deerTotemPool;
    public ObjectPool<GameObject> birdTotemPool;
    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        skill_1_Timer = DataManager.instance.shaman_Skill_Data.skill_1_CD;
        skill_2_Timer = DataManager.instance.shaman_Skill_Data.skill_2_CD;
    }
    protected virtual void Update()
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