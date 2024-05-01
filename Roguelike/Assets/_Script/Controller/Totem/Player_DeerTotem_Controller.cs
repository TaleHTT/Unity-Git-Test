﻿using UnityEngine;

public class Player_DeerTotem_Controller : DeerTotem_Controller
{
    [HideInInspector] public float damage;
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void Update()
    {
        base.Update();
        if (timer < 0)
        {
            Treat();
            timer = 1;
        }
    }
    private void OnDisable()
    {
        if (SkillManger.instance.shaman_Skill.isHave_X_Equipment == true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.shaman_Skill_Data.skill_1_radius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<EnemyBase>() != null)
                {
                    hit.GetComponent<EnemyStats>().TakeDamage(damage);
                    hit.GetComponent<EnemyBase>().isHit = true;
                }
            }
        }
    }
    public void Treat()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.shaman_Skill_Data.skill_1_radius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                PlayerBase target = hit.GetComponent<PlayerBase>();
                hit.GetComponent<PlayerStats>().TakeTreat(treat);
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
        }
    }
}