using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class Player_Summons_Base : PlayerBase
{
    public Seeker seeker;
    public float chaseRadius;
    public GameObject chaseTarget;
    public List<GameObject> chaseTargets;
    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();
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
        EnemyDetect();
        chaseDetect();
        CloestTargetDetect();
    }
    public void enemyDetect()
    {
        chaseTargets = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chaseRadius, whatIsEnemy);
        foreach (var enemy in colliders)
        {
            if (enemy.GetComponent<EnemyBase>() != null)
                chaseTargets.Add(enemy.gameObject);
        }
    }
    public void chaseDetect()
    {
        chaseTarget = null;
        float distance = Mathf.Infinity;
        for (int i = 0; i < chaseTargets.Count; i++)
        {
            if (distance > Vector3.Distance(chaseTargets[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(chaseTargets[i].transform.position, transform.position);
                chaseTarget = chaseTargets[i];
            }
        }
    }
}