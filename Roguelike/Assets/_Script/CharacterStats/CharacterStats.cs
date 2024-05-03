using UnityEngine;
public class CharacterStats : MonoBehaviour
{
    public bool isUnconquered {  get; set; }
    public bool isDefens {  get; set; }
    public int defensNum { get; set; }
    public bool isUseSkill {  get; set; }
    public int level;
    [Tooltip("����ֵ")]
    public int experience;
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
    [Tooltip("�����ٶ�")]
    public Stats attackSpeed;
    [Tooltip("���Ʒ�Χ")]
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
