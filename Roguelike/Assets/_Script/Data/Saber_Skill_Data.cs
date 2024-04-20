using UnityEngine;
[CreateAssetMenu(fileName = "Saber_Skill", menuName = "Skill/Saber_Skill")]
public class Saber_Skill_Data : ScriptableObject
{
    [Tooltip("额外增加的血量")]
    [Range(0, 1)] public float extraAddHp;
    [Tooltip("额外增加的护甲")]
    [Range(0, 1)] public float extraAddArmor;
    [Tooltip("额外增加的伤害")]
    [Range(0, 1)] public float extraAddDamage;
    [Tooltip("减少的速度")]
    [Range(0, 1)] public float moveSpeesDecreased;
    [Tooltip("额外增加的反伤距离")]
    public float extraAddAttackRadius;
    [Tooltip("持续时间")]
    public float persistentTimer;
    [Tooltip("静止时间")]
    public float standTimer;
    [Tooltip("最大受击数")]
    public int maxNumOfHit;
}