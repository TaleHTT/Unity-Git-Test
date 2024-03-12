using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int level;
    public float currentHealth;
    public Stats maxHp;
    public Stats damage;
    public Stats armor;
    public Stats damageMultiplier;
    public Stats moveSpeed;
    public Stats attackRadius;
    public Stats attackSpeed;
    public virtual void Start()
    {
        currentHealth = maxHp.GetValue();
    }

    public virtual void Update()
    {
        
    }
    public virtual void DoDamage(CharacterStats targetstats)
    {
        float totaldamage = damage.GetValue() + level - armor.GetValue() * damageMultiplier.GetValue();
        targetstats.meleeTakeDamage(totaldamage);
    }
    public virtual void meleeTakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    public virtual void remoteTakeDamage(float damage)
    {
        currentHealth -= (damage + level - armor.GetValue() * damageMultiplier.GetValue());
    }
}
