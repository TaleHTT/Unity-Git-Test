using UnityEngine;
public class CharacterStats : MonoBehaviour, ITakeDamageable
{
    [SerializeField] private int level;
    [Tooltip("经验值")]
    public int experience;
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
    [Tooltip("治疗范围")]
    public Stats treatRadius;
    public virtual void Start()
    {
        currentHealth = maxHp.GetValue();
    }

    public virtual void Update()
    {
        if(currentHealth > maxHp.GetValue())
            currentHealth = maxHp.GetValue();
    }

    public virtual void remoteTakeDamage(float damage)
    {
        currentHealth -= ((damage + level - armor.GetValue()) * woundedMultiplier.GetValue());
    }

    public virtual void treatTakeDamage(float damage)
    {
        currentHealth += damage + level;
    }

    public virtual void meleeTakeDamage(float damage)
    {
        currentHealth -= (damage + level - armor.GetValue()) * woundedMultiplier.GetValue();
    }
}
