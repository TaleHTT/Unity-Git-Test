using UnityEngine;
public class CharacterStats : MonoBehaviour
{
    public bool isUnconquered {  get; set; }
    public bool isDefens {  get; set; }
    public int defensNum { get; set; }
    public bool isUseSkill {  get; set; }
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
    public virtual void TakeDamage(float damage, float percentage)
    {
        if (isUnconquered)
            return;
        if(isDefens)
        {
            defensNum--;
            return;
        }
        if (currentHealth <= 1 && isUseSkill)
            return;
        currentHealth -= damage * (1 - armor.GetValue() * percentage / (10 + armor.GetValue() * percentage));   
    }
    public virtual void AuthenticTakeDamage(float damage)
    {
        if (isUnconquered)
            return;
        if (isDefens)
        {
            defensNum--;
            return;
        }
        if (currentHealth <= 1 && isUseSkill)
            return;
        currentHealth -= damage;
    }

    public virtual void TakeTreat(float damage)
    {
        currentHealth += damage + level;
    }
}
