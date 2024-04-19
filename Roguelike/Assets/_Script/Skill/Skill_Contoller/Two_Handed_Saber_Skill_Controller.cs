using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Two_Handed_Saber_Skill_Controller : MonoBehaviour
{
    private float timer;
    private float cdTimer;
    private bool isUseSkill;
    private List<GameObject> bleedDetect;
    public int numOfAttack {  get; set; }
    Player_TwoHandedSaber player_TwoHandedSaber;
    private void Awake()
    {
        bleedDetect = new List<GameObject>();
        cdTimer = DataManager.instance.two_Handed_Saber_Skill_Data.CD;
        player_TwoHandedSaber = GetComponent<Player_TwoHandedSaber>();
        timer = DataManager.instance.two_Handed_Saber_Skill_Data.skill_2_DurationTimer;
    }
    private void Start()
    {
        if (SkillManger.instance.two_Handed_Saber_Skill.isHave_X_Equipment)
        {
            player_TwoHandedSaber.stats.attackSpeed.AddModfiers(DataManager.instance.two_Handed_Saber_Skill_Data.enemy_ExtraAddAttackSpeed * player_TwoHandedSaber.enemyDetects.Count);
            player_TwoHandedSaber.stats.attackSpeed.AddModfiers(DataManager.instance.two_Handed_Saber_Skill_Data.num_ExtraAddAttackSpeed * numOfAttack);
        }        
    }
    private void Update()
    {
        if (player_TwoHandedSaber.isAttack == false)
            StartCoroutine(RecoverAttackSpeed());
        cdTimer -= Time.deltaTime;
        if (player_TwoHandedSaber.stats.currentHealth / player_TwoHandedSaber.stats.maxHp.GetValue() < 0.5f && cdTimer <= 0)
        {
            StartCoroutine(SkillDuration());
            SwordAttack();
        }
        TreatHp();
    }
    public void BleedDectec()
    {
        float radius = Mathf.Infinity;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, player_TwoHandedSaber.whatIsEnemy);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber > 0)
                bleedDetect.Add(hit.gameObject);
        }
    }
    public void TreatHp()
    {
        for(int i = 0; i < bleedDetect.Count; i++)
        {
            if (bleedDetect[i].GetComponent<EnemyBase>().isDead == true)
            {
                player_TwoHandedSaber.stats.currentHealth *= (1 + DataManager.instance.two_Handed_Saber_Skill_Data.recoverHp);
                bleedDetect.Remove(bleedDetect[i]);
                if (bleedDetect[i].GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber <= 0)
                    bleedDetect.Remove(bleedDetect[i]);
            }
        }
    }
    public void SwordAttack()
    {
        if (isUseSkill == false)
            return;
        else
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f / DataManager.instance.two_Handed_Saber_Skill_Data.times);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player_TwoHandedSaber.attackRadius * 2, player_TwoHandedSaber.whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                hit.GetComponent<EnemyStats>().TakeDamage(player_TwoHandedSaber.stats.damage.GetValue() * (1 + DataManager.instance.two_Handed_Saber_Skill_Data.extraAddDamage));
                hit.GetComponent<EnemyBase>().layersOfBleeding_Two_Handed_Saber++;
            }
        }
    }

    public IEnumerator SkillDuration()
    {
        isUseSkill = true;
        yield return new WaitForSeconds(timer);
        isUseSkill = false;
    }
    public IEnumerator RecoverAttackSpeed()
    {
        yield return new WaitForSeconds(3);
        player_TwoHandedSaber.stats.attackSpeed.RemoveModfiers(DataManager.instance.two_Handed_Saber_Skill_Data.enemy_ExtraAddAttackSpeed * player_TwoHandedSaber.enemyDetects.Count);
        player_TwoHandedSaber.stats.attackSpeed.RemoveModfiers(DataManager.instance.two_Handed_Saber_Skill_Data.num_ExtraAddAttackSpeed * numOfAttack);
    }
}