using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerOccupation
{
    Saber,
    Archer,
    Caster,
    Priest
}
public class PlayerBase : Base
{
    [Header("Occupation info")]
    public PlayerOccupation occupation;

    [Header("Attack Layer info")]
    public LayerMask whatIsEnemy;

    [Header("Show Range info")]
    [Tooltip("ÊÇ·ñÏÔÊ¾¹¥»÷·¶Î§")]
    [SerializeField] private bool drawTheBorderOrNot;

    public Transform closetEnemy {  get; set; }
    public List<GameObject> enemyDetects { get; set; }

    [SerializeField] public bool canBreakAwayFromTheTeam { get; set; } = false;
    public PlayerStateMachine stateMachine { get; set; }
    protected override void Awake()
    {
        base.Awake();
        enemyDetects = new List<GameObject>();
        stateMachine = new PlayerStateMachine();
    }
    protected override void Start()
    {
        base.Start();
        isDead = false;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        EnemyDetect();
        CloestTargetDetect();
    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    public void CloestTargetDetect()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < enemyDetects.Count; i++)
        {
            if (distance > Vector3.Distance(enemyDetects[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(enemyDetects[i].transform.position, transform.position);
                closetEnemy = enemyDetects[i].transform;
            }
        }
    }
    public void EnemyDetect()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsEnemy);
        foreach (var enemy in colliders)
        {
            enemyDetects.Add(enemy.gameObject);
        }
    }
    public override void DamageEffect()
    {
        base.DamageEffect();
    }
    public virtual void AnimationArcherAttack()
    {

    }
    public virtual void AnimationCasterAttack()
    {

    }
    public virtual void AnimationPriestAttack()
    {
        
    }

    public virtual void AnimationIceCasterAttack()
    {
        
    }

    public virtual void AnimationBloodsuckerAttack()
    {

    }
}

