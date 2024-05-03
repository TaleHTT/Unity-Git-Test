using UnityEngine;

public class SkillManger : MonoBehaviour
{
    public static SkillManger instance;
    public Saber_Skill saber_Skill { get; set; }
    public Slime_Skill slime_Skill { get; set; }
    public Priest_Skill priest_Skill { get; set; }
    public Caster_Skill caster_Skill { get; set; }
    public Archer_Skill archer_Skill { get; set; }
    public Shaman_Skill shaman_Skill { get; set; }
    public Assassin_Skill assassin_Skill { get; set; }
    public IceCaster_Skill iceCaster_Skill { get; set; }
    public Two_Handed_Saber_Skill two_Handed_Saber_Skill { get; set; }
    public Bloodsucker_Skill bloodsucker_Skill { get; set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        slime_Skill = GetComponent<Slime_Skill>();
        saber_Skill = GetComponent<Saber_Skill>();
        caster_Skill = GetComponent<Caster_Skill>();
        priest_Skill = GetComponent<Priest_Skill>();
        archer_Skill = GetComponent<Archer_Skill>();
        shaman_Skill = GetComponent<Shaman_Skill>();
        assassin_Skill = GetComponent<Assassin_Skill>();
        iceCaster_Skill = GetComponent<IceCaster_Skill>();
        bloodsucker_Skill = GetComponent<Bloodsucker_Skill>();
        two_Handed_Saber_Skill = GetComponent<Two_Handed_Saber_Skill>();
    }
}
