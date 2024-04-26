using System.Collections.Generic;
using System.Drawing;
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
    public bool drawTheBorderOrNot;

    public Transform closetEnemy;

    public List<GameObject> enemyDetects;

    [SerializeField] public bool canBreakAwayFromTheTeam { get; set; } = false;
    public PlayerStateMachine stateMachine { get; set; }
    protected override void Awake()
    {
        base.Awake();
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
    }
    private void FixedUpdate()
    {
        EnemyDetect();
        CloestTargetDetect();
    }
    public virtual void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    public void CloestTargetDetect()
    {
        closetEnemy = null;
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
    public virtual void EnemyDetect()
    {
        enemyDetects = new List<GameObject>();
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
    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}

