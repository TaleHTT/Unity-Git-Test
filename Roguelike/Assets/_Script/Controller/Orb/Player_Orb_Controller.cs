using System.Collections.Generic;
using UnityEngine;

public class Player_Orb_Controller : Orb_Controller
{
    public float num;
    protected override void OnEnable()
    {
        base.OnEnable();
        AttackTarget();
        AttackDir();
        transform.position = Vector3.one;
        isStrengthen = false;
    }
    private void Start()
    {
        num = caster_Skill_Controller.numberOfAttack;
    }
    protected override void Update()
    {
        base.Update();
        if(num >= DataManager.instance.caster_Skill_Data.maxNumberOfAttack)
        {
            transform.localScale = new Vector2(2, 2);
            isStrengthen = true;
            caster_Skill_Controller.numberOfAttack = 0;
            num = caster_Skill_Controller.numberOfAttack;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (isStrengthen == true)
            {
                burningTransform = collision.transform;
                burningRingsPool.Get();
            }
            AttackTakeDamage();
            orbPool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    public void AttackTarget()
    {
        attackDetects = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var target in colliders)
        {
            if (target.GetComponent<EnemyBase>() != null)
            {
                attackDetects.Add(target.transform);
                AttackLogic();
            }
        }
    }
    public void AttackTakeDamage()
    {
        if(isStrengthen == false)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<EnemyStats>() != null)
                {
                    hit.GetComponent<EnemyStats>()?.AuthenticTakeDamage(damage);
                    if (SkillManger.instance.caster_Skill.isHave_X_Equipment == true)
                        hit.GetComponent<EnemyBase>().layersOfBurning++;
                }
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.skill_1_explodeRadius);
            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<EnemyStats>() != null)
                {
                    hit.GetComponent<EnemyStats>()?.AuthenticTakeDamage(strengthExplosionDamage);
                    if (SkillManger.instance.caster_Skill.isHave_X_Equipment == true)
                    {
                        hit.GetComponent<EnemyBase>().layersOfBurning++;
                    }
                }
            }
        }
    }
}
