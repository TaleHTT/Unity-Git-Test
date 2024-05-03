using UnityEngine;
[CreateAssetMenu(fileName = "Priest_Skill", menuName = "Skill/Priest_Skill")]
public class Priest_Skill_Data : ScriptableObject
{
    public float healBaseValue;
    public float damageBaseValue;
    [Range(0, 10)] public float resurrectionHpPercent;
    [Range(0, 10)] public float extraAddHeal;
    [Range(0, 10)] public float extraAddDamage;
    public int maxNumberOfTreatments;
    public int numberOfRespawns;
    public float treatRadius;
    public float CD;
}