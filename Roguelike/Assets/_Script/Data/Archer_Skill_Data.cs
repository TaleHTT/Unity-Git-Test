using UnityEngine;
[CreateAssetMenu(fileName = "Archer_Skill", menuName = "Skill/Archer_Skill")]
public class Archer_Skill_Data : ScriptableObject
{
    public float damageBaseValue;
    [Range(0, 10)] public float extraAddDamage;
    public float skill_1_CD;
    public float skill_2_CD;
}