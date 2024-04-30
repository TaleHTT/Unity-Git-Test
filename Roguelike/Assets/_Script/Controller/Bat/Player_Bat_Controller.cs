using System.Collections.Generic;
using UnityEngine;

public class Player_Bat_Controller : Bat_Controller
{
    [HideInInspector] public Player_Bloodsucker player_Bloodsucker;
    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyDetect();
        AttackDir();
    }
    protected override void Update()
    {
        base.Update();
    }
    public void AttackTarget()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < attackDetects.Count; i++)
        {
            if (distance > Vector2.Distance(transform.position, attackDetects[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, attackDetects[i].transform.position);
                attackTarget = attackDetects[i];
            }
        }
    }
    public void EnemyDetect()
    {
        attackDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                attackDetects.Add(hit.gameObject);
                AttackTarget();
            }
        }
    }
    private void TakeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                hit.GetComponent<EnemyStats>().TakeDamage(damage);
                hit.GetComponent<EnemyBase>().isHit = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            player_Bloodsucker.stats.TakeTreat(damage * (1 + DataManager.instance.bloodsucker_Skill_Data.normalExtraAddHp_2));
            TakeAttack();
            batPool.Release(gameObject);
        }
    }
}