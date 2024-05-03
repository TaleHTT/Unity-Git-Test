using UnityEngine;
[CreateAssetMenu(fileName = "Assassin_Skill", menuName = "Skill/Assassin_Skill")]
public class Assassin_Skill_Data : ScriptableObject
{
    public float healBaseValue;
    public float damageBaseValue;
    [Range(0, 10)] public float extraAddWoundedMultiplier;
    [Range(0, 10)] public float extraAddAttackSpeed;
    [Range(0, 10)] public float extraAddDamage;
    [Range(0, 10)] public float extraAddHp;
    [Range(0, 10)] public float extraMoveSpeed;
    public float skill_1_durationTimer;
    public float skill_2_durationTimer;
    public float skill_3_durationTimer;
    public float radius;
}