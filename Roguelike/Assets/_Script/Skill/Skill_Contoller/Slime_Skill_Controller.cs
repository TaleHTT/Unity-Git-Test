using System.Collections.Generic;
using UnityEngine;

public class Slime_Skill_Controller : Skill_Controller
{
    [HideInInspector] public float duration;
    [HideInInspector] public int maxNumTrigger;
    [HideInInspector] public float skill_1_timer;
    [HideInInspector] public float skill_2_timer;
    
    [HideInInspector] public List<GameObject> teamSlime;
    [HideInInspector] public List<GameObject> slimeDetect;
    public Dictionary<GameObject, float> maxHp = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> armor = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> damage = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, float> attackSpeed = new Dictionary<GameObject, float>();
    protected virtual void Awake()
    {
        teamSlime = new List<GameObject>();
    }
    protected virtual void Start()
    {
        maxNumTrigger = teamSlime.Count;
        skill_1_timer = DataManager.instance.slime_Skill_Data.skill_1_CD;
        skill_2_timer = DataManager.instance.slime_Skill_Data.skill_2_CD;
        duration = DataManager.instance.slime_Skill_Data.duration;       
    }
    protected virtual void Update()
    {
        skill_1_timer -= Time.deltaTime;
        skill_2_timer -= Time.deltaTime;
    }
    protected virtual void FixedUpdate()
    {

    }
}