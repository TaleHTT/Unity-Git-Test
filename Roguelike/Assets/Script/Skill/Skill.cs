using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Transform[] target;
    public float damage;
    private float coolDownTimer;
    public float coolDown;
    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }
    protected virtual bool CanUseSkill()
    {
        if (coolDownTimer <= 0)
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
