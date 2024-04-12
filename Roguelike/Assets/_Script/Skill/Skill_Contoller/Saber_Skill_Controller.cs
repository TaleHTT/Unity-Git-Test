using UnityEngine;

public class Saber_Skill_Controller : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float addArmor;
    [SerializeField][Range(0, 1)] private float extraAddHp;
    [SerializeField][Range(0, 1)] private float extraAddArmor;
    [SerializeField][Range(0, 1)] private float extraAddDamage;
    [SerializeField] private float extraAddAttackRadius;
    [SerializeField] private int maxNumOfHit;
    private Player_Saber player_Saber;
    public int numOfHit;
    public bool isHave_X_Equipment;

    private void Awake()
    {
        player_Saber = GetComponent<Player_Saber>();
    }
    private void Start()
    {
        if (isHave_X_Equipment)
        {
            player_Saber.stats.maxHp.AddModfiers(player_Saber.stats.maxHp.GetValue() * extraAddHp);
            player_Saber.stats.UpdataHp();

            player_Saber.stats.baseDamage.AddModfiers(player_Saber.stats.baseDamage.GetValue() * extraAddDamage);
            player_Saber.stats.baseArmor.AddModfiers(player_Saber.stats.baseArmor.GetValue() * extraAddArmor);
        }        
    }
    private void Update()
    {
        if (player_Saber.isDefense == true)
        {
            player_Saber.stats.actualArmor = player_Saber.stats.baseArmor.AddArmor(addArmor, player_Saber.stats.baseArmor.GetValue());
        }
        else
            player_Saber.stats.actualArmor = player_Saber.stats.baseArmor.GetValue();

        if (numOfHit == maxNumOfHit)
        {
            CounterAttack(player_Saber.stats.actualArmor);
            numOfHit = maxNumOfHit;
        }
    }
    private void CounterAttack(float counterAttackDamage)
    {
        if(isHave_X_Equipment == false)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius);
            foreach(var hit in colliders)
            {
                if(hit.GetComponent<EnemyStats>() != null)
                {
                    hit.GetComponent<EnemyStats>().TakeDamage((1 + addArmor) * counterAttackDamage);
                }
            }
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_Saber.attackRadius + extraAddAttackRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<EnemyStats>() != null)
                {
                    hit.GetComponent<EnemyStats>().TakeDamage((1 + addArmor) * counterAttackDamage);
                }
            }
        }
    }
}
