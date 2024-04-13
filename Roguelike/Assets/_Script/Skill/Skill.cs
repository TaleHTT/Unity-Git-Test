using UnityEngine;

public class Skill : MonoBehaviour
{
    public bool isHave_X_Equipment;
    public float coolDownTimer {  get; set; }
    public float persistentTimer;
    public float coolDown;
    private void Awake()
    {
        coolDownTimer = 5f;
    }
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
