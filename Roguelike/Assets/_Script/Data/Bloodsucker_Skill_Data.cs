using UnityEngine;
[CreateAssetMenu(fileName = "Bloodsucker_Skill", menuName = "Skill/Bloodsucker_Skill")]
public class Bloodsucker_Skill_Data : ScriptableObject
{
    [Range(0, 10)] public float normalExtraAddDamage;
    [Range(0, 10)] public float normalExtraAddHp_1;
    [Range(0, 10)] public float normalExtraAddHp_2;
    [Range(0, 10)] public float skill_1_ExtraRemoveHp;
    [Range(0, 10)] public float skill_1_ExtraAddDamage;
    [Range(0, 10)] public float skill_2_ExtraAddDamage;
    [Range(0, 10)] public float skill_2_ExtraAddHp;
    public float skill_2_Duration;
    public float indexTimerDamage;
    public float skill_2_CD;
    public float indexTimer;
    public float width;
    public float length;

}