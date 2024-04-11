using UnityEngine;

public class SkillManger : MonoBehaviour
{
    public static SkillManger instance;
    public Archer_Skill archer_Skill {  get; set; }
    public Saber_Skill saber_Skill { get; set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    protected virtual void Start()
    {
        archer_Skill = GetComponent<Archer_Skill>();
        saber_Skill = GetComponent <Saber_Skill>();
    }
}
