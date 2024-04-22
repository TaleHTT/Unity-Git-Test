using UnityEngine;
public class CharacterStats : MonoBehaviour
{
    public bool isUseSkill;
    public int level;
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
    [Tooltip("攻击速度")]
    public Stats attackSpeed;
    [Tooltip("治疗范围")]
    public Stats treatRadius;
    public virtual void Start()
    {
        UpdateHp();
    }


    public virtual void Update()
    {
        if(currentHealth > maxHp.GetValue())
            currentHealth = maxHp.GetValue();
    }
    public void UpdateHp()
    {
        currentHealth = maxHp.GetValue();
    }

    public virtual void TakeDamage(float damage)
    {
        if (currentHealth <= 1 && isUseSkill)
            return;
        currentHealth -= ((damage + level - armor.GetValue()) * woundedMultiplier.GetValue());
    }
    public virtual void AuthenticTakeDamage(float damage)
    {
        if (currentHealth <= 1 && isUseSkill)
            return;
        currentHealth -= damage;
    }

    public virtual void TakeTreat(float damage)
    {
        currentHealth += damage + level;
    }
}
