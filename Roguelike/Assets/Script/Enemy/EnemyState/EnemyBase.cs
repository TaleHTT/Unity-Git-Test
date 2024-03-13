using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Entity
{
    [Header("Chase info")]
    [Tooltip("Ë÷µĞ·¶Î§")]
    public float chaseRadius;

    [Tooltip("ËÀÍöºó£¬¾­¹ıtimerÃëºóÏú»ÙÎïÌå")]
    public float timer;
    [Tooltip("ÅĞ¶ÏÊÇ·ñËÀÍö")]
    public bool isDead;
    [Tooltip("ÊÇ·ñÏÔÊ¾¹¥»÷ºÍÑ°µĞ·¶Î§")]
    public bool drawTheBorderOrNot;
    public int targetPointIndex { get; private set; } = 0;
    public List<GameObject> playerDetects;
    public List<GameObject> attackDetects;
    [Tooltip("Ñ²Âßµã")]
    public Transform[] patrolPoints;
    public Transform target {  get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

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
        }
        if(playerDetects.Count == 1)
        {
            target = playerDetects[0].transform;
        }
        else
        {
            ChaseLogic();
        }
    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, stats.attackRadius.GetValue());
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
    public void DamageEffect()
    {
        Debug.Log("I am damage");
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual void playerDetect()
    {
        detectTimer -= Time.deltaTime;
        if (detectTimer > 0)
        {
            detectTimer = 1;
            return;
        }
        playerDetects = new List<GameObject>();
        var colliders = Physics2D.OverlapCircleAll(transform.position, chaseRadius, whatIsEnemy);
        foreach (var player in colliders)
        {
            playerDetects.Add(player.gameObject);
        }
    }
    public virtual void AnimationArcherAttack()
    {
        
    }
    private void ChaseLogic()
    {
        if (playerDetects.Count >= 3)
        {
            for (int i = 1; i < playerDetects.Count - 1; i++)
            {
                target = ((Vector2.Distance(transform.position, playerDetects[i].transform.position) > Vector2.Distance(transform.position, playerDetects[i + 1].transform.position)) ? ((Vector2.Distance(transform.position, playerDetects[i].transform.position) > Vector2.Distance(transform.position, playerDetects[i - 1].transform.position)) ? playerDetects[i].transform : playerDetects[i - 1].transform) : ((Vector2.Distance(transform.position, playerDetects[i + 1].transform.position) > Vector2.Distance(transform.position, playerDetects[i - 1].transform.position)) ? playerDetects[i + 1].transform : playerDetects[i - 1].transform));
            }
        }
        else if (playerDetects.Count == 2)
        {
            for (int i = 0; i < playerDetects.Count - 1; i++)
            {
                if (Vector2.Distance(transform.position, playerDetects[i].transform.position) >
                    Vector2.Distance(transform.position, playerDetects[i + 1].transform.position))
                {
                    target = playerDetects[i].transform;
                }
                else
                {
                    target = playerDetects[i + 1].transform;
                }
            }
        }
    }
    IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
    public void AttackLogic()
    {
        if (playerDetects.Count >= 3)
        {
            for (int i = 1; i < playerDetects.Count - 1; i++)
            {
                stats.DoDamage((Vector2.Distance(transform.position, playerDetects[i].transform.position) > Vector2.Distance(transform.position, playerDetects[i + 1].transform.position)) ? ((Vector2.Distance(transform.position, playerDetects[i].transform.position) > Vector2.Distance(transform.position, playerDetects[i - 1].transform.position)) ? playerDetects[i].GetComponent<EnemyStats>() : playerDetects[i - 1].GetComponent<EnemyStats>()) : ((Vector2.Distance(transform.position, playerDetects[i + 1].transform.position) > Vector2.Distance(transform.position, playerDetects[i - 1].transform.position)) ? playerDetects[i + 1].GetComponent<EnemyStats>() : playerDetects[i - 1].GetComponent<EnemyStats>()));
            }
        }
        else if (playerDetects.Count == 2)
        {
            for (int i = 0; i < playerDetects.Count - 1; i++)
            {
                if (Vector2.Distance(transform.position, playerDetects[i].transform.position) >
                    Vector2.Distance(transform.position, playerDetects[i + 1].transform.position))
                {
                    playerDetects[i].GetComponent<EnemyStats>();
                    stats.DoDamage(playerDetects[i].GetComponent<EnemyStats>());

                }
                else
                {
                    playerDetects[i + 1].GetComponent<EnemyStats>();
                    stats.DoDamage(playerDetects[i].GetComponent<EnemyStats>());
                }
            }
        }
    }
}
