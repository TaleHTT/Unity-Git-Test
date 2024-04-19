using UnityEngine;
[CreateAssetMenu(fileName = "Assassin_Skill", menuName = "Skill/Assassin_Skill")]
public class Assassin_Skill_Data : ScriptableObject
{
    [Range(0, 1)] public float extraAddWoundedMultiplier;
    [Range(0, 1)] public float extraAddAttackSpeed;
    [Range(0, 1)] public float extraAddDamage;
    [Range(0, 1)] public float extraAddHp;
    public float skill_1_durationTimer;
    public float skill_2_durationTimer;
    public float skill_3_durationTimer;
    public float radius;
}