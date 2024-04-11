using UnityEngine;

public class Saber_Skill_Controller : MonoBehaviour
{
    [Range(0, 1)] private float addArmor;
    [SerializeField] private int maxNumOfHit {  get; set; }
    public int numOfHit { get; set; }

    Player_Saber player_Saber;
    private void Awake()
    {
        player_Saber = GetComponent<Player_Saber>();
    }
    private void Update()
    {
        if (SkillManger.instance.saber_Skill.CanUseSkill())
        {
            if (player_Saber.isDefense == true)
            {
                player_Saber.stats.armor.AddArmor(addArmor);
            }
        }
        if(numOfHit == maxNumOfHit)
        {
            if (player_Saber.isDefense == true)
                CounterAttack(player_Saber.stats.armor.AddArmor(addArmor));
            else if (player_Saber.isDefense == false)
                CounterAttack(player_Saber.stats.armor.GetValue());
            numOfHit = maxNumOfHit;
        }
    }
    private void CounterAttack(float counterAttackDamage)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.stats.attackRadius.GetValue());
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<EnemyStats>() != null)
            {
                hit.GetComponent<EnemyStats>().TakeDamage(counterAttackDamage);
            }
        }
    }
}
