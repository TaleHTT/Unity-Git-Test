using System.Collections.Generic;
using UnityEngine;

public class Priest_Skill_Controller : Skill_Controller
{
    public float attackRidus;

    [HideInInspector] public float timer;
    [HideInInspector] public int randomNum;
    [HideInInspector] public int numOfRespawns;
    [HideInInspector] public GameObject[] treatTarget;
    [HideInInspector] public List<GameObject> treatDetect;
    [HideInInspector] public List<GameObject> teamMender = new List<GameObject>();

    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        timer = DataManager.instance.priest_Skill_Data.CD;
    }
    protected virtual void Update()
    {
        timer -= Time.deltaTime;
    }
    protected virtual void FixedUpdate()
    {
      
    }
}