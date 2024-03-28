using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int level;
    [Tooltip("当前血量")]
    public float currentHealth;
    [Tooltip("最大生命值")]
    public Stats maxHp;
    [Tooltip("攻击力")]
    public Stats damage;
    [Tooltip("护甲")]
    public Stats armor;
    [Tooltip("受伤倍率")]
    public Stats woundedMultiplier;
    [Tooltip("移动速度")]
    public Stats moveSpeed;
    [Tooltip("攻击范围")]
    public Stats attackRadius;
    [Tooltip("攻击速度")]
    public Stats attackSpeed;
    public virtual void Start()
    {
        currentHealth = maxHp.GetValue();
    }

    public virtual void Update()
    {
        
    }
    public virtual void meleeDoDamage(CharacterStats targetstats)
    {
        float totaldamage = (damage.GetValue() + level - armor.GetValue()) * woundedMultiplier.GetValue();
        targetstats.meleeTakeDamage(totaldamage);
    }
    public virtual void treatDoDamage(CharacterStats targetstats)
    {
        float totaltreat = (damage.GetValue() + level);
        targetstats.treatTakeDamage(totaltreat);
    }
    public virtual void meleeTakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    public virtual void remoteTakeDamage(float damage)
    {
        currentHealth -= ((damage + level - armor.GetValue()) * woundedMultiplier.GetValue());
    }

    public virtual void treatTakeDamage(float damage)
    {
        currentHealth += damage;
    }
}
