using UnityEngine;

public class Skill : MonoBehaviour
{
    public float coolDownTimer {  get; set; }
    public float persistentTimer;
    public float coolDown;
    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }
    public virtual bool CanUseSkill()
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
