using System.Collections.Generic;
using UnityEngine;

public class Two_Handed_Saber_Skill_Controller : Skill_Controller
{
    [HideInInspector] public float addSpeed;
    [HideInInspector] public float baseValue;
    [HideInInspector] public float timer = 1;
    [HideInInspector] public int numOfAttacks;
    [HideInInspector] public List<GameObject> bleedDetect;
    [HideInInspector] public List<GameObject> enemyDetect;
    protected virtual void Awake()
    {
        bleedDetect = new List<GameObject>();
    }
    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {

    }
}