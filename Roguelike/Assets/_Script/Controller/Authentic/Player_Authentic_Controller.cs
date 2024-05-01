using UnityEngine;

public class Player_Authentic_Controller : Authentic_Controller
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            collision.GetComponent<EnemyStats>()?.AuthenticTakeDamage(damage);
            collision.GetComponent<EnemyBase>().isHit = true;
            authenticPool.Release(gameObject);
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