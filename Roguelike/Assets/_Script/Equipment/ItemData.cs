using UnityEngine;
public enum Shape
{
    regular,
    L,
    samllL,
    T,
    samllT,
    samllZ,
    Z,
    U,
    X,
    Rootnumber
}
[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public Shape shape;
    public int width = 1;
    public int height = 1;
    public Sprite itemIcon;
    [Tooltip("�������ֵ")]
    public int maxHp;
    [Tooltip("������")]
    public int damage;
    [Tooltip("����")]
    public int armor;
    [Tooltip("���˱���")]
    public int woundedMultiplier;
    [Tooltip("�ƶ��ٶ�")]
    public int moveSpeed;
    [Tooltip("������Χ")]
    public int attackRadius;
    [Tooltip("�����ٶ�")]
    public int attackSpeed;
    public void AddModfiers(PlayerStats stats)
    {
        stats.maxHp.AddModfiers(maxHp);
        stats.damage.AddModfiers(damage);
        stats.armor.AddModfiers(armor);
        stats.woundedMultiplier.AddModfiers(woundedMultiplier);
        stats.moveSpeed.AddModfiers(moveSpeed);
        stats.attackRadius.AddModfiers(attackRadius);
        stats.attackSpeed.AddModfiers(attackSpeed);
    }
    public void RemoveModfiers(PlayerStats stats)
    {
        stats.maxHp.RemoveModfiers(maxHp);
        stats.damage.RemoveModfiers(damage);
        stats.armor.RemoveModfiers(armor);
        stats.woundedMultiplier.RemoveModfiers(woundedMultiplier);
        stats.moveSpeed.RemoveModfiers(moveSpeed);
        stats.attackRadius.RemoveModfiers(attackRadius);
        stats.attackSpeed.RemoveModfiers(attackSpeed);
    }
}
