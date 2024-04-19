using UnityEngine;
[CreateAssetMenu(fileName = "Shaman_Skill", menuName = "Skill/Shaman_Skill")]
public class Shaman_Skill_Data : ScriptableObject
{
    [Range(0, 1)] public float skill_1_ExtraAddTreatHp;
    [Range(0, 1)] public float normal_ExtraTreatHp;
    [Range(0, 1)] public float extraAddAttackSpeed;
    [Range(0, 1)] public float extraAddMoveSpeed;
    [Range(0, 1)] public float extraAddDamage;
    [Range(0, 1)] public float skill_1_AddHp;
    [Range(0, 1)] public float skill_2_AddHp;
    public float skill_1_duration;
    public float skill_2_duraiton;
    public float skill_1_radius;
    public float skill_2_radius;
    public float skill_1_CD;
    public float skill_2_CD;
}