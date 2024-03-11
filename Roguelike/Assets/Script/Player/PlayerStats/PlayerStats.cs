using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    PlayerBase player;
    public override void Start()
    {
        base.Start();
        player = GetComponent<PlayerBase>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        player.DamageEffect();
    }
}
