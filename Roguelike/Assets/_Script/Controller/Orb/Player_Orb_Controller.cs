using UnityEngine;

public class Player_Orb_Controller : Orb_Controller
{
    protected override void OnEnable()
    {
        base.OnEnable();
        AttackTarget();
        AttackDir();
    }
    protected override void Update()
    {
        base.Update();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            AttackTakeDamage();
            if (isStrengthen == true)
                burningRingsPool.Get();
            orbPool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    public void AttackTarget()
    {
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
                }
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, strengthExplosionRadius);
            foreach (Collider2D hit in colliders)
            {
                if (hit.GetComponent<EnemyStats>() != null)
                {
                    hit.GetComponent<EnemyStats>()?.AuthenticTakeDamage(strengthExplosionDamage);
                    if(SkillManger.instance.caster_Skill.isHave_X_Equipment == true)
                        hit.GetComponent<EnemyBase>().

                }
            }
        }
    }
}
