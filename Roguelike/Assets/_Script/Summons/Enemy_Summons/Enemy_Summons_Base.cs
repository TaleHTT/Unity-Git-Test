using System.Collections.Generic;
using UnityEngine;

public class Enemy_Summons_Base : Summons_Base
{
    public LayerMask whatIsPlayer;
    public List<GameObject> playerDetects;
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
        for (int i = 0; i < playerDetects.Count; i++)
        {
            if (distance > Vector2.Distance(transform.position, playerDetects[i].transform.position))
            {
                distance = Vector2.Distance(transform.position, playerDetects[i].transform.position);
                cloestTarget = playerDetects[i].transform;
            }
        }
    }
    public void enemyDetect()
    {
        playerDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chaseRadius, whatIsPlayer);
        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<PlayerBase>() != null)
                playerDetects.Add(enemy.gameObject);
        }
    }
    public void attackDetect()
    {
        attackDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsPlayer);
        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<PlayerBase>() != null)
                attackDetects.Add(enemy.gameObject);
        }
    }
}