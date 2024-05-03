using UnityEngine;
[CreateAssetMenu(fileName = "Priest_Skill", menuName = "Skill/Priest_Skill")]
public class Priest_Skill_Data : ScriptableObject
{
    [Range(0, 10)] public float resurrectionHpPercent;
    [Range(0, 10)] public float extraAddHeal;
    public int maxNumberOfTreatments;
    public int numberOfRespawns;
    public float authenticDamage;
    public float treatRadius;
    public float CD;
}