using UnityEngine;
public class CharacterStats : MonoBehaviour
{
    [Tooltip("��Ѫ�˺�")]
    public float bleedingDamage;
    public int level;
    [Tooltip("����ֵ")]
    public int experience;
    [Tooltip("�������ֵ")]
    public Stats maxHp;
    [Tooltip("��ǰѪ��")]
    public float currentHealth;
    [Tooltip("������")]
    public Stats baseDamage;
    public float actualDamage;
    [Tooltip("����")]
    public Stats baseArmor;
    public float actualArmor;
    [Tooltip("���˱���")]
    public Stats woundedMultiplier;
    [Tooltip("�ƶ��ٶ�")]
    public Stats moveSpeed;
    [Tooltip("������Χ")]
    public Stats attackRadius;
    [Tooltip("�����ٶ�")]
    public Stats attackSpeed;
    [Tooltip("���Ʒ�Χ")]
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
