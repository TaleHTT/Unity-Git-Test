using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManger : MonoBehaviour
{ 
    public static SkillManger instance;
    private Archer_Skill_Controller archer_Skill;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    protected virtual void Start()
    {
        archer_Skill = GetComponent<Archer_Skill_Controller>();
    }
}
