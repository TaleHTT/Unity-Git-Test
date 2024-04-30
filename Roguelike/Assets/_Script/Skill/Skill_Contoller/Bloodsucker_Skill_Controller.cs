using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bloodsucker_Skill_Controller : Skill_Controller
{
    public GameObject parasitismBatDefensPrefab;
    public GameObject parasitismBatAttackPrefab;
    public GameObject rectanglePrefab;
    [HideInInspector] public ObjectPool<GameObject> parasitismBatAttackPool;
    [HideInInspector] public ObjectPool<GameObject> parasitismBatDefensPool;
    [HideInInspector] public ObjectPool<GameObject> rectanglePool;
    
    public int currentNum;
    public int currentBlood;
    
    [HideInInspector] public Vector2 attackDir;
    [HideInInspector] public int maxBatNum;
    [HideInInspector] public float maxBlood;
    [HideInInspector] public float index = 0;
    [HideInInspector] public float indexTimer;
    [HideInInspector] public float skill_2_Timer;
    [HideInInspector] public List<GameObject> skillDetect;
    
    [HideInInspector] public float timer = 1;
    [HideInInspector] public float duration;
    [HideInInspector] public int parasitismBatNum;

    protected virtual void Awake()
    {

    }
    protected virtual void Start()
    {
        duration = DataManager.instance.bloodsucker_Skill_Data.skill_2_Duration;
        skill_2_Timer = DataManager.instance.bloodsucker_Skill_Data.skill_2_CD;
        indexTimer = DataManager.instance.bloodsucker_Skill_Data.indexTimer;
    }
    protected virtual void Update()
    {
        
    }
    public void ActionRectangleOnGet(GameObject rectangle)
    {
        rectangle.transform.position = new Vector2((transform.position.x + DataManager.instance.bloodsucker_Skill_Data.length / 2), transform.position.y);
        rectangle.SetActive(true);
    }
    public void ActionParasitismBatAttackOnGet(GameObject bat)
    {
        bat.transform.position = transform.position;
        bat.SetActive(true);
    }
    public void ActionParasitismBatDefensOnGet(GameObject bat)
    {
        bat.transform.position = transform.position;
        bat.SetActive(true);
    }
    public void ActionOnRelease(GameObject bat)
    {
        bat.SetActive(false);
    }
    public void ActionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}