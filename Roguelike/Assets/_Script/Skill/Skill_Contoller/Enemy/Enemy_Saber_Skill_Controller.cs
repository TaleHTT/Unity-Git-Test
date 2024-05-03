using UnityEngine;

public class Enemy_Saber_Skill_Controller : Saber_Skill_Controller
{
    [HideInInspector] public Enemy_Saber enemy_Saber;
    protected override void Awake()
    {
        base.Awake();
        enemy_Saber = GetComponent<Enemy_Saber>();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        if (numOfHit == saber_Skill_Data.maxNumOfHit)
        {
            CounterAttack(enemy_Saber.stats.armor.GetValue());
            numOfHit = 0;
        }
    }
    private void CounterAttack(float counterAttackDamage)
    {
        if (SkillManger.instance.saber_Skill.isHave_X_Equipment == true && saberDetect.Count == 1 && isZeroPosition == true)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, enemy_Saber.attackRadius);
        }
        else
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, enemy_Saber.attackRadius * (1 + saber_Skill_Data.extraAddAttackRadius));
        }
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerStats>() != null)
            {
                hit.GetComponent<PlayerStats>().TakeDamage(saber_Skill_Data.counterBaseValue + saber_Skill_Data.extraAddArmor * counterAttackDamage);
            }
        }
    }
}