using System.Collections.Generic;
using UnityEngine;

public class Enemy_Priest_Skill_Controller : Priest_Skill_Controller
{
    private Enemy_Priest enemy_Priest;
    protected override void Awake()
    {
        base.Awake();
        enemy_Priest = GetComponent<Enemy_Priest>();
    }
    protected override void Start()
    {
        base.Start();

        if (enemy_Priest.stats.level <= 5)
            treatTarget = new GameObject[enemy_Priest.stats.level];
        else
            treatTarget = new GameObject[5];
        TeamDetect();
        for (int i = 0; i < teamMender.Count; i++)
        {
            if (teamMender[i].GetComponent<Enemy_Priest>() != null)
                teamMender.Remove(teamMender[i]);
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        RespawnsEnemy();
        TreatDetect();
        TreatTarget();
        TreatSkill();
    }
    public void RespawnsEnemy()
    {
        if (enemy_Priest.isDead)
            return;
        for (int i = 0; i < teamMender.Count; i++)
        {
            EnemyBase target = teamMender[i].GetComponent<EnemyBase>();
            if (target.isDead == true && numOfRespawns < DataManager.instance.priest_Skill_Data.numberOfRespawns)
            {
                Debug.Log("Eneter");
                target.isDead = false;
                target.stats.currentHealth = DataManager.instance.priest_Skill_Data.resurrectionHpPercent * enemy_Priest.stats.maxHp.GetValue();
                numOfRespawns++;
            }
        }
    }
    public void TeamDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.priest_Skill_Data.treatRadius);
        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<EnemyBase>() != null)
            {
                teamMender.Add(enemy.gameObject);
            }
        }
    }
    public void TreatDetect()
    {
        treatDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.priest_Skill_Data.treatRadius);
        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<EnemyBase>() != null)
            {
                treatDetect.Add(enemy.gameObject);
            }
        }
        for (int i = 0; i < treatDetect.Count; i++)
        {
            if (treatDetect[i].GetComponent<Enemy_Priest>() != null)
                treatDetect.Remove(treatDetect[i]);
        }
    }
    public void TreatTarget()
    {
        for (int i = 0; i < treatDetect.Count - 1; i++)
        {
            if (treatDetect[i].GetComponent<EnemyStats>().currentHealth >= treatDetect[i + 1].GetComponent<EnemyStats>().currentHealth)
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
                if (treatTarget[i].GetComponent<EnemyBase>().isDead)
                {
                    continue;
                }
                EnemyBase target = treatTarget[i].GetComponent<EnemyBase>();
                treatTarget[i].GetComponent<EnemyStats>()?.TakeTreat((DataManager.instance.priest_Skill_Data.extraAddHeal + 1) * enemy_Priest.stats.maxHp.GetValue());
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
                        case 3:
                            target.layersOfBurning = 0;
                            break;
                    }
                }
            }
            timer = DataManager.instance.priest_Skill_Data.CD;
        }
    }
}