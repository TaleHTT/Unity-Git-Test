using UnityEngine;
[CreateAssetMenu(fileName = "Slime_Skill", menuName = "Skill/Slime_Skill")]
public class Slime_Skill_Data : ScriptableObject
{
    [Range(0, 1)] public float skill_X_ExtraAddAttackRadius;
    [Range(0, 1)] public float skill_X_ExtraAddAttackSpeed;
    [Range(0, 1)] public float skill_2_ExtraAddAttackSpeed;
    [Range(0, 1)] public float skill_X_ExtraAddDamage;
    [Range(0, 1)] public float skill_2_ExtraAddDamage;
    [Range(0, 1)] public float skill_X_ExtraAddArmor;
    [Range(0, 1)] public float skill_2_ExtraAddArmor;
    [Range(0, 1)] public float skill_1_ExtraAddHp;
    [Range(0, 1)] public float skill_2_ExtraAddHp;
    [Range(0, 1)] public float skill_X_ExtraAddHp;
    public float skill_1_CD;
    public float skill_2_CD;
    public float duration;
    public float radius;
}