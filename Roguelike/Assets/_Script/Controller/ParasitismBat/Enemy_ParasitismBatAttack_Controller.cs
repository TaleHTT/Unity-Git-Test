using System.Collections.Generic;
using UnityEngine;

public class Enemy_ParasitismBatAttack_Controller : ParasitismBatAttack_Controller
{
    public Enemy_Bloodsucker_Skill_Controller enemy_Bloodsucker_Skill_Controller;
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void Update()
    {
        base.Update();
        PlayerDetect();
        AttackTarget();
        if (isBack == true)
            transform.position = Vector2.MoveTowards(transform.position, enemy_Bloodsucker_Skill_Controller.enemy_Bloodsucker.transform.position, moveSpeed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, cloestTarget.transform.position, moveSpeed * Time.deltaTime);
    }
    public void AttackTarget()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < attackDetect.Count; i++)
        {
            if (distance > Vector2.Distance(transform.position, attackDetect[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, attackDetect[i].transform.position);
                cloestTarget = attackDetect[i];
            }
        }
    }
    public void PlayerDetect()
    {
        attackDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
                attackDetect.Add(hit.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(enemy_Bloodsucker_Skill_Controller.enemy_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_2_ExtraAddDamage);
            collision.GetComponent<PlayerBase>().isHit = true;
            isBack = true;
        }
        if (isBack == true)
        {
            if (collision.gameObject.tag == "Bloodsucker")
            {
                collision.GetComponent<Player_Bloodsucker>().stats.TakeTreat(enemy_Bloodsucker_Skill_Controller.enemy_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_2_ExtraAddHp);
                parasitismBatPool.Release(gameObject);
                enemy_Bloodsucker_Skill_Controller.parasitismBatNum--;
                enemy_Bloodsucker_Skill_Controller.currentBlood++;
            }
        }
    }
}