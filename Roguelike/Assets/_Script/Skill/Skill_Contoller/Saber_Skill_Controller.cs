using System.Collections.Generic;
using UnityEngine;

public class Saber_Skill_Controller : Skill_Controller
{
    public bool isZeroPosition;
    public Saber_Skill_Data saber_Skill_Data;

    [HideInInspector] public int numOfHit;
    [HideInInspector] public Collider2D[] colliders;
    [HideInInspector] public List<GameObject> saberDetect;

    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
       
    }
    protected virtual void Update()
    {

    }
}
