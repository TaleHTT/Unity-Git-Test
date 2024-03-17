using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillManger : MonoBehaviour
{
    public PlayerBase player;
    public Transform[] target;
    public float treat;
    public float damage;
    public float coolDownTimer;
    public float coolDown;
    public SkillManger instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    protected virtual void Start()
    {
        player = GetComponent<PlayerBase>();
    }
    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseSkill()
    {
        if(coolDown <= 0)
        {
            UseSkill();
            coolDownTimer = coolDown;
            return true;
        }
        return false;
    }
    public virtual void UseSkill()
    {

    }
}
