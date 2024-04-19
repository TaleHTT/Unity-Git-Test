using System.Collections.Generic;
using UnityEngine;

public class Saber_Skill_Controller : MonoBehaviour
{
    public int numOfHit { get; set; }
    public bool isZeroPosition { get; set; }
    public float detectRadius {  get; set; }

    public Saber_Skill_Data saber_Skill_Data;
    public Collider2D[] colliders {  get; set; }
    public Player_Saber player_Saber {  get; set; }
    public List<GameObject> saberDetect {  get; set; }

    private void Awake()
    {
        player_Saber = GetComponent<Player_Saber>();
    }
    private void Start()
    {
        if (SkillManger.instance.saber_Skill.isHave_X_Equipment == true && saberDetect.Count == 1 && isZeroPosition == true)
        {
            player_Saber.stats.armor.AddModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddArmor);

            player_Saber.stats.maxHp.AddModfiers(player_Saber.stats.maxHp.GetValue() * saber_Skill_Data.extraAddHp);
            player_Saber.stats.UpdataHp();   
        }        
    }
    private void Update()
    {
        if (player_Saber.isDefense == true)
            player_Saber.stats.armor.AddModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddArmor);
        else
            player_Saber.stats.armor.RemoveModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddArmor);

        if(SkillManger.instance.saber_Skill.isHave_X_Equipment == true && saberDetect.Count == 1 && isZeroPosition == true)
            player_Saber.stats.damage.AddModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddDamage);

        if (numOfHit == saber_Skill_Data.maxNumOfHit)
        {
            CounterAttack(player_Saber.stats.armor.GetValue());
            numOfHit = 0;
        }
    }
    private void SaberDetect()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectRadius);
        foreach(var saber in colliders)
        {
            if (saber.GetComponent<Player_Saber>() != null)
                saberDetect.Add(saber.gameObject);
        }
    }
    private void CounterAttack(float counterAttackDamage)
    {
        if(SkillManger.instance.saber_Skill.isHave_X_Equipment == true && saberDetect.Count == 1 && isZeroPosition == true)
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius);
        }
        else
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius * (1 + saber_Skill_Data.extraAddAttackRadius));
        }
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyStats>() != null)
            {
                hit.GetComponent<EnemyStats>().TakeDamage((1 + saber_Skill_Data.extraAddArmor) * counterAttackDamage);
            }
        }
    }
}
