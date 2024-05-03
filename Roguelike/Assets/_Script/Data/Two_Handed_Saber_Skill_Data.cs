using UnityEngine;
[CreateAssetMenu(fileName = "Two_Handed_Saber_Skill", menuName = "Skill/Two_Handed_Saber_Skill")]
public class Two_Handed_Saber_Skill_Data : ScriptableObject
{
    [Range(0, 10)] public float enemy_ExtraAddAttackSpeed;
    [Range(0, 10)] public float num_ExtraAddAttackSpeed;
    [Range(0, 10)] public float extraAddDamage;
    [Range(0, 10)] public float depleteHp;
    [Range(0, 10)] public float recoverHp;
    public float skill_1_DurationTimer;
    public float skill_2_DurationTimer;
    public float bleedDamage;
    public float times;
    public float CD;
}