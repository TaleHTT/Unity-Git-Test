using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public Saber_Skill_Data saber_Skill_Data;
    public Hound_Skill_Data hound_Skill_Data;
    public Slime_Skill_Data slime_Skill_Data;
    public Archer_Skill_Data archer_Skill_Data;
    public Priest_Skill_Data priest_Skill_Data;
    public Caster_Skill_Data caster_Skill_Data;
    public Shaman_Skill_Data shaman_Skill_Data;
    public Assassin_Skill_Data assassin_Skill_Data;
    public IceCaster_Skill_Data iceCasterSkill_Data;
    public Bloodsucker_Skill_Data bloodsucker_Skill_Data;
    public Two_Handed_Saber_Skill_Data two_Handed_Saber_Skill_Data;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
}
