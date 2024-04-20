using UnityEngine;
[CreateAssetMenu(fileName = "Saber_Skill", menuName = "Skill/Saber_Skill")]
public class Saber_Skill_Data : ScriptableObject
{
    [Tooltip("�������ӵ�Ѫ��")]
    [Range(0, 1)] public float extraAddHp;
    [Tooltip("�������ӵĻ���")]
    [Range(0, 1)] public float extraAddArmor;
    [Tooltip("�������ӵ��˺�")]
    [Range(0, 1)] public float extraAddDamage;
    [Tooltip("���ٵ��ٶ�")]
    [Range(0, 1)] public float moveSpeesDecreased;
    [Tooltip("�������ӵķ��˾���")]
    public float extraAddAttackRadius;
    [Tooltip("����ʱ��")]
    public float persistentTimer;
    [Tooltip("��ֹʱ��")]
    public float standTimer;
    [Tooltip("����ܻ���")]
    public int maxNumOfHit;
}