using System.Collections.Generic;
using UnityEngine;

public class Enemy_IceOrb_Controller : IceOrb_Controller
{
    [HideInInspector] public Enemy_IceCaster enemy_IceCaster;
    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyDetect();
        MoveDir();
    }
    protected override void Update()
    {
        base.Update();
    }
    public void EnemyDetect()
    {
        attackDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                attackDetect.Add(hit.gameObject);
            }
        }
        AttackTarget();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyBase enemyBase = collision.gameObject.GetComponent<EnemyBase>();
            if (enemyBase.isFreeze == false)
            {
                enemyBase.layerOfCold++;
                collision.GetComponent<PlayerStats>().TakeDamage(damage);
                collision.GetComponent<PlayerBase>().isHit = true;
                enemyBase.timer_Cold = 3;
            }
            else
            {
                enemyBase.isHitInFreeze = true;
                enemyBase.layerOfCold = 0;
                collision.GetComponent<PlayerStats>().TakeDamage(damage * enemy_IceCaster.stats.maxHp.GetValue());
                collision.GetComponent<PlayerBase>().isHit = true;
            }
            orbPool.Release(gameObject);
        }
    }
}