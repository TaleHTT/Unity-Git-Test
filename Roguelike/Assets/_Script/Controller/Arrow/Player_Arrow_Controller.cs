using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Arrow_Controller : Arrow_Controller
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
            collision.GetComponent<EnemyStats>()?.TakeDamage(damage);
            pool.Release(gameObject);
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