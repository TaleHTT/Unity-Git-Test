using System.Collections.Generic;
using UnityEngine;

public class Enemy_BirdTotem_Controller : BirdTotem_Controller
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void Awake()
    {
        base.Awake();
        PlayerDetect();
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        AddMoveSpeed();
        AddAttackSpeed();
    }
    private void AddAttackSpeed()
    {
        for (int i = 0; i < targetDetect.Count; i++)
        {
            EnemyStats player = targetDetect[i].GetComponent<EnemyStats>();
            if (Vector2.Distance(transform.position, targetDetect[i].transform.position) <= DataManager.instance.shaman_Skill_Data.skill_2_radius)
            {
                if (attackSpeed.TryGetValue(targetDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.attackSpeed.GetValue();
                    attackSpeed.Add(targetDetect[i], player.attackSpeed.GetValue());
                    player.attackSpeed.AddModfiers(baseValue * DataManager.instance.shaman_Skill_Data.extraAddAttackSpeed);
                }
            }
            else
            {
                if (attackSpeed.TryGetValue(targetDetect[i], out float value))
                {
                    player.attackSpeed.RemoveModfiers(value * DataManager.instance.shaman_Skill_Data.extraAddAttackSpeed);
                }
            }
        }
    }

    private void AddMoveSpeed()
    {
        for (int i = 0; i < targetDetect.Count; i++)
        {
            EnemyStats player = targetDetect[i].GetComponent<EnemyStats>();
            if (Vector2.Distance(transform.position, targetDetect[i].transform.position) <= DataManager.instance.shaman_Skill_Data.skill_2_radius)
            {
                if (moveSpeed.TryGetValue(targetDetect[i], out float value))
                {
                    continue;
                }
                else
                {
                    float baseValue = player.moveSpeed.GetValue();
                    moveSpeed.Add(targetDetect[i], player.moveSpeed.GetValue());
                    player.moveSpeed.AddModfiers(baseValue * DataManager.instance.shaman_Skill_Data.extraAddMoveSpeed);
                }
            }
            else
            {
                if (moveSpeed.TryGetValue(targetDetect[i], out float value))
                {
                    player.moveSpeed.RemoveModfiers(value * DataManager.instance.shaman_Skill_Data.extraAddMoveSpeed);
                }
            }
        }
    }
    public void PlayerDetect()
    {
        targetDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.shaman_Skill_Data.skill_2_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                targetDetect.Add(hit.gameObject);
        }
    }
}