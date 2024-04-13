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
    public PlayerOccupation occupation;

    [Header("Attack info")]
    public LayerMask whatIsEnemy;

    [Tooltip("¹¥»÷·¶Î§")]
    public float attackRadius;

    [Tooltip("ËÀÍöºó£¬¾­¹ýtimerÃëºóÏú»Ù")]
    [SerializeField] public float timer {  get; set; }

    [Tooltip("ÊÇ·ñËÀÍö")]
    [SerializeField] public bool isDead {  get; set; }

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
        if (isDead)
        {
            StartCoroutine(DeadDestroy(timer));
            return;
        }
        else
        {
            gameObject.SetActive(true);
        }
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
        detectTimer -= Time.deltaTime;
        if (detectTimer > 0)
        {
            detectTimer = 1;
            return;
        }
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsEnemy);
        foreach (var enemy in colliders)
        {
            enemyDetects.Add(enemy.gameObject);
        }
    }
    public IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
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
}

