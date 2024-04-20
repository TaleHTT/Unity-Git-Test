using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ParasitismBatAttack_Controller : MonoBehaviour
{
    public ObjectPool<GameObject> parasitismBatPool {  get; set; }
    public Player_Bloodsucker player_Bloodsucker { get; set; }
    public Bloodsucker_Skill_Controller bloodsucker_Skill_Controller { get; set; }
    List<GameObject> enemyDetect;
    GameObject cloestTarget;
    public float moveSpeed;
    bool isBack = false;
    private void Update()
    {
        EnemyDetect();
        AttackTarget();
        if (isBack == true)
            transform.position = Vector2.MoveTowards(transform.position, player_Bloodsucker.transform.position, moveSpeed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, cloestTarget.transform.position, moveSpeed * Time.deltaTime);
    }
    public void AttackTarget()
    {
        float distance = Mathf.Infinity;
        for(int i = 0; i < enemyDetect.Count; i++)
        {
            if(distance > Vector2.Distance(transform.position, enemyDetect[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, enemyDetect[i].transform.position);
                cloestTarget = enemyDetect[i];
            }
        }
    }
    public void EnemyDetect()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
                enemyDetect.Add(hit.gameObject);
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Eenmy"))
        {
            collision.GetComponent<EnemyStats>().TakeDamage(player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_2_ExtraAddDamage);
            isBack = true;
        }
        if(isBack == true)
        {
            if(collision.gameObject.tag == "Bloodsucker")
            {
                collision.GetComponent<Player_Bloodsucker>().stats.TakeTreat(player_Bloodsucker.stats.maxHp.GetValue() * DataManager.instance.bloodsucker_Skill_Data.skill_2_ExtraAddHp);
                parasitismBatPool.Release(gameObject);
                bloodsucker_Skill_Controller.parasitismBatNum--;
            }
        }
    }
}