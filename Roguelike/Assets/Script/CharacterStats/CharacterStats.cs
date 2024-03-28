using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private int level;
    [Tooltip("��ǰѪ��")]
    public float currentHealth;
    [Tooltip("�������ֵ")]
    public Stats maxHp;
    [Tooltip("������")]
    public Stats damage;
    [Tooltip("����")]
    public Stats armor;
    [Tooltip("���˱���")]
    public Stats woundedMultiplier;
    [Tooltip("�ƶ��ٶ�")]
    public Stats moveSpeed;
    [Tooltip("������Χ")]
    public Stats attackRadius;
    [Tooltip("�����ٶ�")]
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
