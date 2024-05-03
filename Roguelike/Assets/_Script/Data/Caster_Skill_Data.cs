using UnityEngine;
[CreateAssetMenu(fileName = "Caster_Skill", menuName = "Skill/Caster_Skill")]
public class Caster_Skill_Data : ScriptableObject
{
    [Range(0, 10)] public float skill_1_extraAddExplodeDamage;
    [Range(0, 10)] public float skill_2_extraAddExplodeDamage;
    [Range(0, 10)] public float extraAddBruningDamage;
    [Range(0, 10)] public float extraAddDamage;
    public float skill_1_explodeRadius;
    public float skill_2_explodeRadius;
    public int maxNumberOfAttack;
    public float duration;
    public float radius;
    public float CD;
}