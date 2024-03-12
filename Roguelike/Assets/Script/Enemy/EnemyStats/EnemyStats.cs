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
    public override void meleeTakeDamage(float damage)
    {
        base.meleeTakeDamage(damage);
        enemy.DamageEffect();
    }
}
