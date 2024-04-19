using UnityEngine;
[CreateAssetMenu(fileName = "IceCaster_Skill", menuName = "Skill/IceCaster_Skill")]
public class IceCaster_Skill_Data : ScriptableObject
{
    [Range(0, 1)] public float skill_1_ExtraAddDamage;
    [Range(0, 1)] public float skill_2_ExtraAddDamage;
    public float durationTimer;
    public float attack_Speed;
    public float skill_1_radius;
    public float skill_2_radius;
    public float skill_1_CD;
    public float skill_2_CD;
}