using UnityEngine;
public class CharacterStats : MonoBehaviour, ITakeDamageable
{
    [SerializeField] private int level;
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
    [Tooltip("������Χ")]
    public Stats attackRadius;
    [Tooltip("�����ٶ�")]
    public Stats attackSpeed;
    [Tooltip("���Ʒ�Χ")]
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

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= ((damage + level - armor.GetValue()) * woundedMultiplier.GetValue());
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
