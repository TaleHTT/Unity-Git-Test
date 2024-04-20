using System.Collections.Generic;
using UnityEngine;

public class Saber_Skill_Controller : MonoBehaviour
{
    public bool isZeroPosition;
    public int numOfHit;

    public Saber_Skill_Data saber_Skill_Data;
    public Collider2D[] colliders {  get; set; }
    public Player_Saber player_Saber {  get; set; }
    public List<GameObject> saberDetect {  get; set; }

    private void Awake()
    {
        player_Saber = GetComponent<Player_Saber>();
        SaberDetect();
    }
    private void Start()
    {
        if (SkillManger.instance.saber_Skill.isHave_X_Equipment == true && saberDetect.Count == 1 && isZeroPosition == true)
        {
            player_Saber.stats.damage.AddModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddDamage);
            player_Saber.stats.armor.AddModfiers(player_Saber.stats.armor.GetValue() * saber_Skill_Data.extraAddArmor);
            player_Saber.stats.maxHp.AddModfiers(player_Saber.stats.maxHp.GetValue() * saber_Skill_Data.extraAddHp);
            player_Saber.stats.UpdateHp();   
        }        
    }
    private void Update()
    {
        if (numOfHit == saber_Skill_Data.maxNumOfHit)
        {
            CounterAttack(player_Saber.stats.armor.GetValue());
            numOfHit = 0;
        }
    }
    private void SaberDetect()
    {
        saberDetect = new List<GameObject>();
        colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
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
            colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius/*Mathf.Infinity*/);
        }
        else
        {
            colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius/*Mathf.Infinity*/ * (1 + saber_Skill_Data.extraAddAttackRadius));
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
