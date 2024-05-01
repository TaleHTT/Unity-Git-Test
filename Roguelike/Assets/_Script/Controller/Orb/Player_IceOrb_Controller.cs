using System.Collections.Generic;
using UnityEngine;

public class Player_IceOrb_Controller : IceOrb_Controller
{
    [HideInInspector] public Player_IceCaster player_IceCaster;
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
            if (hit.GetComponent<EnemyBase>() != null)
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
                collision.GetComponent<EnemyStats>().TakeDamage(damage);
                collision.GetComponent<EnemyBase>().isHit = true;
                enemyBase.timer_Cold = 3;
            }
            else
            {
                enemyBase.isHitInFreeze = true;
                enemyBase.layerOfCold = 0;
                collision.GetComponent<EnemyStats>().TakeDamage(damage * player_IceCaster.stats.maxHp.GetValue());
                collision.GetComponent<EnemyBase>().isHit = true;
            }
            orbPool.Release(gameObject);
        }
    }
}