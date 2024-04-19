using System.Collections.Generic;
using UnityEngine;

public class Priest_Skill_Controller : MonoBehaviour
{
    private float timer;
    public float attackRidus;
    private Player_Priest player_Priest;
    public int numberOfTreatments { get; set; }
    public GameObject[] treatTarget {  get; set; }
    public List<GameObject> treatDetect;


    private void Awake()
    {
        timer = DataManager.instance.priest_Skill_Data.CD;
        player_Priest = GetComponent<Player_Priest>();
        if (player_Priest.stats.level <= 5)
            treatTarget = new GameObject[player_Priest.stats.level];
        else
            treatTarget = new GameObject[5];
    }
    private void Update()
    {
        if (player_Priest.isDead)
            return;
        timer -= Time.deltaTime;
        if (SkillManger.instance.priest_Skill.isHave_X_Equipment == true)
        {
            if (numberOfTreatments >= DataManager.instance.priest_Skill_Data.maxNumberOfTreatments)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRidus, DataManager.instance.priest_Skill_Data.whatIsEnemy);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<EnemyBase>() != null)
                        hit.GetComponent<EnemyStats>().AuthenticTakeDamage(DataManager.instance.priest_Skill_Data.authenticDamage);
                }
            }
            numberOfTreatments = 0;
        }
        RespawnsPlayer();
        TreatDetect();
        TreatTarget();
        TreatSkill();
    }
    public void RespawnsPlayer()
    {
        if (player_Priest.isDead)
            return;
        for (int i = 0; i < treatDetect.Count; i++)
        {
            if (treatDetect[i].GetComponent<PlayerBase>().isDead == true && DataManager.instance.priest_Skill_Data.numberOfRespawns != 0)
            {
                treatDetect[i].GetComponent<PlayerBase>().isDead = false;
                treatDetect[i].GetComponent<PlayerBase>().stats.currentHealth = DataManager.instance.priest_Skill_Data.resurrectionHpPercent * player_Priest.stats.maxHp.GetValue();
                DataManager.instance.priest_Skill_Data.numberOfRespawns--;
            }
        }
    }
    public void TreatDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.priest_Skill_Data.treatRadius, DataManager.instance.priest_Skill_Data.whatIsPlayer);
        foreach (var player in colliders)
        {
            if (player.GetComponent<PlayerBase>() != null)
            {
                treatDetect.Add(player.gameObject);
                if (player.GetComponent<Priest_Skill>() != null)
                {
                    treatDetect.Remove(player.gameObject);
                }
            }
        }
    }
    public void TreatTarget()
    {
        for(int i = 0;i < treatDetect.Count - 1;i++)
        {
            if (treatDetect[i].GetComponent<PlayerStats>().currentHealth >= treatDetect[i + 1].GetComponent<PlayerStats>().currentHealth)
            {
                GameObject temp = treatDetect[i];
                treatDetect[i] = treatDetect[i + 1];
                treatDetect[i + 1] = temp;
            }
        }
        for(int i = 0;i < treatTarget.Length; i++)
        {
            treatTarget[i] = treatDetect[i];
        }
    }
    private void TreatSkill()
    {
        if (treatTarget != null && timer <= 0)
        {
            for (int i = 0; i < treatTarget.Length; i++)
            {
                treatTarget[i].GetComponent<PlayerStats>()?.TakeTreat((DataManager.instance.priest_Skill_Data.extraAddHeal + 1) * player_Priest.stats.maxHp.GetValue());
                numberOfTreatments++;
            }
            timer = DataManager.instance.priest_Skill_Data.CD;
        }
    }
}