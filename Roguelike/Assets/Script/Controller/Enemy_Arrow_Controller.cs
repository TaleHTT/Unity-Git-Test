using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Arrow_Controller : MonoBehaviour
{
    public Transform firstTarget;
    public List<Transform> firstTargetDetects;
    public List<Transform> attackDetects;
    public Transform attackTarget;
    public Vector3 arrowDir;
    public float attackRadius;
    public float moveSpeed;
    public float damage;
    private void Awake()
    {
        AttackTarget();
        arrowDir = (attackTarget.position - transform.position).normalized;
    }
    private void Start()
    {

    }
    public void Update()
    {
        transform.Translate(arrowDir * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.GetComponent<CharacterStats>()?.remoteTakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void AttackTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius);
        foreach(var target in colliders)
        {
            if(target.GetComponent<PlayerBase>() != null)
            {
                attackDetects.Add(target.transform);
                if(attackDetects.Count == 1)
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
    private void AttackLogic()
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
