using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private PlayerBase player;
    public override void Start()
    {
        base.Start();
        player = GetComponent<PlayerBase>();
    }
    public override void meleeTakeDamage(float damage)
    {
        base.meleeTakeDamage(damage);
        player.DamageEffect();
    }
}
