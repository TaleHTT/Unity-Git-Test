using UnityEngine;

public class Player_Arrow_Controller : Arrow_Controller
{
    protected override void OnEnable()
    {
        base.OnEnable();
        AttackTarget();
        ArrowDir();
    }
    protected override void Update()
    {
        base.Update();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (SkillManger.instance.archer_Skill.isHave_X_Equipment == true)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
                pool.Release(gameObject);
            return;
        }
        else
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                collision.GetComponent<PlayerStats>()?.TakeDamage(damage);
                pool.Release(gameObject);
            }
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
}