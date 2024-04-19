using UnityEngine;
[CreateAssetMenu(fileName = "Priest_Skill", menuName = "Skill/Priest_Skill")]
public class Priest_Skill_Data : ScriptableObject
{
    [Range(0, 1)] public float resurrectionHpPercent;
    [Range(0, 1)] public float extraAddHeal;
    public int maxNumberOfTreatments;
    public int numberOfRespawns = 1;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnemy;
    public float authenticDamage;
    public float treatRadius;
    public float CD;
}