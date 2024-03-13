using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Arrow_Controller : MonoBehaviour
{
    [Tooltip("移动速度")]
    public float moveSpeed;
    [Tooltip("伤害")]
    public float damage;
    [Tooltip("经过timer秒后箭矢自动销毁")]
    public float timer;
    public List<Transform> attackDetects;
    private float attackRadius = 10000000000;
    private Transform attackTarget;
    private Vector3 arrowDir;
    protected void Awake()
    {
        AttackTarget();
        arrowDir = (attackTarget.position - transform.position).normalized;
    }
    protected void Update()
    {
        transform.Translate(arrowDir * moveSpeed * Time.deltaTime);
        timer -= Time.deltaTime;
        if (timer < 0)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.GetComponent<CharacterStats>()?.remoteTakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void AttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach (var target in colliders)
        {
            if (target.GetComponent<EnemyBase>() != null)
            {
                attackDetects.Add(target.transform);
                if (attackDetects.Count == 1)
                {
                    attackTarget = attackDetects[0];
                }
                else
                {
                    AttackLogic();
                }
            }
        }
    }
    public void AttackLogic()
    {
        if (attackDetects.Count >= 3)
        {
            for (int i = 1; i < attackDetects.Count - 1; i++)
            {
                attackTarget = ((Vector2.Distance(transform.position, attackDetects[i].transform.position) > Vector2.Distance(transform.position, attackDetects[i + 1].transform.position)) ? ((Vector2.Distance(transform.position, attackDetects[i].transform.position) > Vector2.Distance(transform.position, attackDetects[i - 1].transform.position)) ? attackDetects[i].transform : attackDetects[i - 1].transform) : ((Vector2.Distance(transform.position, attackDetects[i + 1].transform.position) > Vector2.Distance(transform.position, attackDetects[i - 1].transform.position)) ? attackDetects[i + 1].transform : attackDetects[i - 1].transform));
            }
        }
        else if (attackDetects.Count == 2)
        {
            for (int i = 0; i < attackDetects.Count - 1; i++)
            {
                if (Vector2.Distance(transform.position, attackDetects[i].transform.position) >
                    Vector2.Distance(transform.position, attackDetects[i + 1].transform.position))
                {
                    attackTarget = attackDetects[i].transform;
                }
                else
                {
                    attackTarget = attackDetects[i + 1].transform;
                }
            }
        }
    }
}
