using Unity.VisualScripting.Antlr3.Runtime.Misc;
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
    public void AddModfiers()
    {
        PlayerStats playerStats = FindObjectOfType(typeof(PlayerStats)) as PlayerStats;
        playerStats.maxHp.AddModfiers(maxHp);
        playerStats.damage.AddModfiers(damage);
        playerStats.armor.AddModfiers(armor);
        playerStats.woundedMultiplier.AddModfiers(woundedMultiplier);
        playerStats.moveSpeed.AddModfiers(moveSpeed);
        playerStats.attackRadius.AddModfiers(attackRadius);
        playerStats.attackSpeed.AddModfiers(attackSpeed);
    }
    public void RemoveModfiers()
    {
        PlayerStats playerStats = FindObjectOfType(typeof(PlayerStats)) as PlayerStats;
        playerStats.maxHp.RemoveModfiers(maxHp);
        playerStats.damage.RemoveModfiers(damage);
        playerStats.armor.RemoveModfiers(armor);
        playerStats.woundedMultiplier.RemoveModfiers(woundedMultiplier);
        playerStats.moveSpeed.RemoveModfiers(moveSpeed);
        playerStats.attackRadius.RemoveModfiers(attackRadius);
        playerStats.attackSpeed.RemoveModfiers(attackSpeed);
    }
}
