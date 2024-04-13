using UnityEngine;
public class CharacterStats : MonoBehaviour
{
    [Tooltip("流血伤害")]
    public float bleedingDamage;
    public int level;
    [Tooltip("经验值")]
    public int experience;
    [Tooltip("最大生命值")]
    public Stats maxHp;
    [Tooltip("当前血量")]
    public float currentHealth;
    [Tooltip("攻击力")]
    public Stats baseDamage;
    public float actualDamage;
    [Tooltip("护甲")]
    public Stats baseArmor;
    public float actualArmor;
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
    private void Awake()
    {
        actualArmor = baseArmor.GetValue();
        actualDamage = baseDamage.GetValue();
    }
    public virtual void Start()
    {
        UpdataHp();
    }


    public virtual void Update()
    {
        if(currentHealth > maxHp.GetValue())
            currentHealth = maxHp.GetValue();
    }
    public void UpdataHp()
    {
        currentHealth = maxHp.GetValue();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= ((damage + level - actualArmor) * woundedMultiplier.GetValue());
    }
    public virtual void AuthenticTakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public virtual void TakeTreat(float damage)
    {
        currentHealth += damage + level;
    }
}
