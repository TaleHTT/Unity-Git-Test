using System.Collections.Generic;
using UnityEngine;

public class Player_Summons_Base : Summons_Base
{
    public LayerMask whatIsEnemy;
    public List<GameObject> enemyDetects;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        isDead = false;
    }
    protected override void Update()
    {
        base.Update();
    }
    private void FixedUpdate()
    {
        enemyDetect();
        attackDetect();
        CloestTargetDetect();
    }
    public void CloestTargetDetect()
    {
        cloestTarget = null;
        float distance = Mathf.Infinity;
        for (int i = 0; i < enemyDetects.Count; i++)
        {
            if (distance > Vector2.Distance(transform.position, enemyDetects[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, enemyDetects[i].transform.position);
                cloestTarget = enemyDetects[i].transform;
            }
        }
    }
    public void enemyDetect()
    {
        enemyDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chaseRadius, whatIsEnemy);
        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<EnemyBase>() != null)
                enemyDetects.Add(enemy.gameObject);
        }
    }
    public void attackDetect()
    {
        attackDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsEnemy);
        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<EnemyBase>() != null)
                attackDetects.Add(enemy.gameObject);
        }
    }
}