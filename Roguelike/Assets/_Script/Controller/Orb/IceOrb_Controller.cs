using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class IceOrb_Controller : MonoBehaviour
{
    public float moveSpeed;
    public float damage;
    List<GameObject> attackDetect;
    GameObject attackTarget;
    Vector2 attckDir;
    public float timer;
    private float coolDownTimer;
    public ObjectPool<GameObject> orbPool;
    public Player_IceCaster player_IceCaster;
    private void OnEnable()
    {
        EnemyDetect();
        MoveDir();
        coolDownTimer = timer;
    }
    private void Update()
    {
        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer < 0)
        {
            orbPool.Release(gameObject);
            attackDetect.Clear();
        }
        transform.Translate(attckDir * moveSpeed * Time.deltaTime);
    }
    public void MoveDir() => attckDir = (attackTarget.transform.position - transform.position).normalized;
    public void EnemyDetect()
    {
        attackDetect = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity);
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<EnemyBase>() != null)
            {
                attackDetect.Add(hit.gameObject);
            }
        }
        AttackTarget();
    }
    public void AttackTarget()
    {
        float distance = Mathf.Infinity;
        for(int i = 0; i < attackDetect.Count; i++)
        {
            if(distance > Vector2.Distance(transform.position, attackDetect[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, attackDetect[i].transform.position);
                attackTarget = attackDetect[i]; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            EnemyBase enemyBase = collision.gameObject.GetComponent<EnemyBase>();
            if(enemyBase.isFreeze == false)
            {
                enemyBase.layerOfCold++;
                collision.GetComponent<EnemyStats>().TakeDamage(damage);
                enemyBase.timer_Cold = 3;
            }
            else
            {
                enemyBase.isHitInFreeze = true;
                enemyBase.layerOfCold = 0;
                collision.GetComponent<EnemyStats>().TakeDamage(damage * player_IceCaster.stats.maxHp.GetValue());
            }
            orbPool.Release(gameObject);
;       }
    }
}