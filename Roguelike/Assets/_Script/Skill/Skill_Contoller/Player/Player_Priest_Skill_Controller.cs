using System.Collections.Generic;
using UnityEngine;

public class Player_Priest_Skill_Controller : Priest_Skill_Controller
{
    private Player_Priest player_Priest;

    [HideInInspector] public int numberOfTreatments;
    public List<GameObject> priest;
    protected override void Awake()
    {
        base.Awake();
        player_Priest = GetComponent<Player_Priest>();
    }
    protected override void Start()
    {
        base.Start();

        if (player_Priest.stats.level <= 5)
            treatTarget = new GameObject[player_Priest.stats.level];
        else
            treatTarget = new GameObject[5];
        TeamDetect();
        for (int i = 0; i < teamMender.Count; i++)
        {
            if (teamMender[i].GetComponent<Player_Priest>() != null)
            {
                teamMender.Remove(teamMender[i]);
                priest.Add(teamMender[i]);
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        if (SkillManger.instance.priest_Skill.isHave_X_Equipment == true)
        {
            float sumHp = 0;
            for(int i = 0; i < priest.Count; i++)
            {
                sumHp += priest[i].GetComponent<Player_Priest>().stats.maxHp.GetValue();
            }
            if (numberOfTreatments >= DataManager.instance.priest_Skill_Data.maxNumberOfTreatments)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRidus);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<EnemyBase>() != null)
                        hit.GetComponent<EnemyStats>().AuthenticTakeDamage(DataManager.instance.priest_Skill_Data.extraAddDamage * sumHp + DataManager.instance.priest_Skill_Data.damageBaseValue);
                }
            }
            numberOfTreatments = 0;
        }
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        RespawnsPlayer();
        TreatDetect();
        TreatTarget();
        TreatSkill();
    }
    public void RespawnsPlayer()
    {
        if (player_Priest.isDead)
            return;
        for (int i = 0; i < teamMender.Count; i++)
        {
            PlayerBase target = teamMender[i].GetComponent<PlayerBase>();
            if (target.isDead == true && numOfRespawns < DataManager.instance.priest_Skill_Data.numberOfRespawns)
            {
                Debug.Log("Eneter");
                target.isDead = false;
                target.stats.currentHealth = DataManager.instance.priest_Skill_Data.resurrectionHpPercent * player_Priest.stats.maxHp.GetValue();
                numOfRespawns++;
            }
        }
    }
    public void TeamDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.priest_Skill_Data.treatRadius);
        foreach (var player in colliders)
        {
            if (player.GetComponent<PlayerBase>() != null)
            {
                teamMender.Add(player.gameObject);
            }
        }
    }
    public void TreatDetect()
    {
        treatDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.priest_Skill_Data.treatRadius);
        foreach (var player in colliders)
        {
            if (player.GetComponent<PlayerBase>() != null)
            {
                treatDetect.Add(player.gameObject);
            }
        }
        for (int i = 0; i < treatDetect.Count; i++)
        {
            if (treatDetect[i].GetComponent<Player_Priest>() != null)
                treatDetect.Remove(treatDetect[i]);
        }
    }
    public void TreatTarget()
    {
        for (int i = 0; i < treatDetect.Count - 1; i++)
        {
            if (treatDetect[i].GetComponent<PlayerStats>().currentHealth >= treatDetect[i + 1].GetComponent<PlayerStats>().currentHealth)
            {
                GameObject temp = treatDetect[i];
                treatDetect[i] = treatDetect[i + 1];
                treatDetect[i + 1] = temp;
            }
        }
        for (int i = 0; i < treatDetect.Count; i++)
        {
            treatTarget[i] = treatDetect[i];
        }
    }
    private void TreatSkill()
    {
        if (treatTarget != null && timer <= 0)
        {
            for (int i = 0; i < treatDetect.Count; i++)
            {
                if (treatTarget[i].GetComponent<PlayerBase>().isDead)
                {
                    continue;
                }
                PlayerBase target = treatTarget[i].GetComponent<PlayerBase>();
                treatTarget[i].GetComponent<PlayerStats>()?.TakeTreat(DataManager.instance.priest_Skill_Data.extraAddHeal * player_Priest.stats.maxHp.GetValue() + DataManager.instance.priest_Skill_Data.healBaseValue);
                Instantiate(GameObjectManager.Instance.treatEffect, treatTarget[i].transform.position, Quaternion.identity, transform);
                numberOfTreatments++;
                if (target.negativeEffect.Count > 0)
                {
                    int a = target.randomNum[Random.Range(0, target.negativeEffect.Count)];
                    target.randomNum.Remove(a);
                    switch (a)
                    {
                        case 0:
                            {
                                target.layersOfBleeding_Hound = 0;
                                target.layersOfBleeding_Two_Handed_Saber = 0;
                            }
                            break;
                        case 1:
                            target.markDurationTimer = 0;
                            break;
                        case 2:
                            target.timer_Cold = 0;
                            break;
                    }
                }
            }
            timer = DataManager.instance.priest_Skill_Data.CD;
        }
    }
}