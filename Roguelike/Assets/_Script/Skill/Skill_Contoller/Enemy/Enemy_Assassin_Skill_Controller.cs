using System.Collections.Generic;
using UnityEngine;

public class Enemy_Assassin_Skill_Controller : Assassin_Skill_Controller
{
    [HideInInspector] public Enemy_Assassin enemy_Assassin;

    protected override void Awake()
    {
        base.Awake();
        enemy_Assassin = GetComponent<Enemy_Assassin>();
    }
    protected override void Start()
    {
        base.Start();
        value = enemy_Assassin.stats.attackSpeed.GetValue();
        baseValue = enemy_Assassin.stats.attackSpeed.baseValue;
    }
    protected override void Update()
    {
        base.Update();
        HuntingDectect();

        if (huntTarget.Count > 0)
            enemy_Assassin.stats.attackSpeed.baseValue = (1 + DataManager.instance.assassin_Skill_Data.extraAddAttackSpeed) * value;
        else
        {
            enemy_Assassin.stats.attackSpeed.baseValue = baseValue;
        }
    }
    public void HuntingDectect()
    {
        huntTarget = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                if (hit.GetComponent<PlayerBase>().isHunting == true)
                    huntTarget.Add(hit.gameObject);
            }
        }
    }
}