using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Priest_Skill_Controller : MonoBehaviour
{
    public float damage;
    [SerializeField][Range(0, 1)] private float resurrectionHpPercent;
    public float amountOfHealing;
    public float treatRadius;
    public GameObject treatTarget;
    private List<GameObject> playerDetect;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsEnemy;
    private int numberOfRespawns = 1;
    public int numberOfTreatments {  get; set; }
    public float attackRidus;
    public int maxNumberOfTreatments;
    private Player_Priest player_Priest;

    private void Awake()
    {
        player_Priest = GetComponent<Player_Priest>();
    }
    private void Update()
    {
        if(SkillManger.instance.priest_Skill.isHave_X_Equipment == true)
        {
            if(numberOfTreatments == maxNumberOfTreatments)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRidus, whatIsEnemy);
                foreach(var hit in colliders)
                {
                    if (hit.GetComponent<EnemyBase>() != null)
                        hit.GetComponent<EnemyStats>().AuthenticTakeDamage(damage);
                }
            }
        }
        RespawnsPlayer();
        PlayerDetect();
        TreatTarget();
    }
    public void RespawnsPlayer()
    {
        for(int i = 0; i < playerDetect.Count; i++)
        {
            if (playerDetect[i].GetComponent<PlayerBase>().isDead == true && numberOfRespawns != 0)
            {
                playerDetect[i].GetComponent<PlayerBase>().isDead = false;
                playerDetect[i].GetComponent<PlayerBase>().stats.currentHealth = resurrectionHpPercent * player_Priest.stats.maxHp.GetValue();
                numberOfRespawns--;
            }
        }
    }
    public void PlayerDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, treatRadius, whatIsPlayer);
        foreach(var player in colliders)
        {
            if(player.GetComponent<PlayerBase>() != null)
            {
                playerDetect.Add(player.gameObject);
            }
        }
    }
    public void TreatTarget()
    {
        float hp = Mathf.Infinity;
        for(int i = 0; i < playerDetect.Count; i++)
        {
            if(hp > playerDetect[i].GetComponent<PlayerStats>().currentHealth)
            {
                hp = playerDetect[i].GetComponent <PlayerStats>().currentHealth;
                treatTarget = playerDetect[i];
            }
        }
    }
}