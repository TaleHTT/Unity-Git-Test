using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    EnemyBase enemy;
    public override void Start()
    {
        base.Start();
        enemy = GetComponent<EnemyBase>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        enemy.DamageEffect();
    }
}
