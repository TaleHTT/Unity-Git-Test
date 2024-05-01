using System.Collections.Generic;
using UnityEngine;

public class Player_ParasitismBatAttack_Controller : ParasitismBatAttack_Controller
{
    public Player_Bloodsucker_Skill_Controller player_Bloodsucker_Skill_Controller;
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void Update()
    {
        base.Update();
        EnemyDetect();
        AttackTarget();
        if (isBack == true)
            transform.position = Vector2.MoveTowards(transform.position, player_Bloodsucker_Skill_Controller.player_Bloodsucker.transform.position, moveSpeed * Time.deltaTime);
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
    public void EnemyDetect()
    {
        attackDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                attackDetect.Add(hit.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.GetComponent<EnemyStats>().TakeDamage(player_Bloodsucker_Skill_Controller.player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_2_ExtraAddDamage);
            collision.GetComponent<EnemyBase>().isHit = true;
            isBack = true;
        }
        if (isBack == true)
        {
            if (collision.gameObject.tag == "Bloodsucker")
            {
                collision.GetComponent<Player_Bloodsucker>().stats.TakeTreat(player_Bloodsucker_Skill_Controller.player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_2_ExtraAddHp);
                parasitismBatPool.Release(gameObject);
                player_Bloodsucker_Skill_Controller.parasitismBatNum--;
                player_Bloodsucker_Skill_Controller.currentBlood++;
            }
        }
    }
}