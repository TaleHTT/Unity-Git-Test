using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bat_Controller : MonoBehaviour
{
    public Player_Bloodsucker player_Bloodsucker {  get; set; }
    public ObjectPool<GameObject> batPool {  get; set; }
    public float damage;
    public float explodeRadius;
    public float moveSpeed;
    public float timer;
    private float coolDownTimer;
    public LayerMask whatIsEnemy;
    List<GameObject> enemyDetects;
    GameObject attackTarget;
    Vector2 attackDir;
    private void OnEnable()
    {
        EnemyDetect();
        AttackDir();
        coolDownTimer = timer;
    }
    private void Update()
    {
        transform.Translate(attackDir * moveSpeed * Time.deltaTime);
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer < 0)
        {
            coolDownTimer = timer;
            batPool.Release(gameObject);
            enemyDetects.Clear();
        }
    }
    public void AttackDir() => attackDir = (attackTarget.transform.position - transform.position).normalized;
    public void AttackTarget()
    {
        float distance = Mathf.Infinity;
        for(int i = 0; i < enemyDetects.Count; i++)
        {
            if(distance > Vector2.Distance(transform.position, enemyDetects[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, enemyDetects[i].transform.position);
                attackTarget = enemyDetects[i];
            }
        }
    }
    public void EnemyDetect()
    {
        enemyDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity, whatIsEnemy);
        foreach(var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                enemyDetects.Add(hit.gameObject);
                AttackTarget();
            }
        }
    }
    private void TakeAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explodeRadius, whatIsEnemy);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                hit.GetComponent<EnemyStats>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            player_Bloodsucker.stats.TakeTreat(damage * (1 + DataManager.instance.bloodsucker_Skill_Data.normalExtraAddHp_2));
            TakeAttack();
            batPool.Release(gameObject);
        }
    }
}