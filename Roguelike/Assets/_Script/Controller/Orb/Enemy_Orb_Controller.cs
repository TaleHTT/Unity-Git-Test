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
            AttackTakeDamage();
            pool.Release(gameObject);
            attackDetects.Clear();
        }
    }
    public void AttackTarget()
    {
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
                hit.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }
}
