using System.Collections.Generic;
using UnityEngine;

public class Enemy_Orb_Controller : Orb_Controller
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if(numberOfPenetrations <= 0)
            {
                AttackTakeDamage();
                attackDetects.Clear();
                orbPool.Release(gameObject);
            }
            else
            {
                numberOfPenetrations--;
                AttackTakeDamage();
            }
        }
    }
    public void AttackTarget()
    {
        attackDetects = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var target in colliders)
        {
            if(target.GetComponent<PlayerBase>() != null)
            {
                attackDetects.Add(target.transform);
                AttackLogic();
            }
        }
    }
    public void AttackTakeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<PlayerStats>() != null)
            {
                hit.GetComponent<PlayerStats>()?.AuthenticTakeDamage(damage);
            }
        }
    }
}
