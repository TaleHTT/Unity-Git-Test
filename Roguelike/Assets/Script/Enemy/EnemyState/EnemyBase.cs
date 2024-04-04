using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyOccupation
{
    Saber,
    Archer,
    Caaster
}
public class EnemyBase : Entity
{
    public EnemyOccupation occupation;
    [Header("Chase info")]
    [Tooltip("索敌范围")]
    public float chaseRadius;

    [Tooltip("死亡后，经过timer秒后销毁物体")]
    public float timer;
    [Tooltip("判断是否死亡")]
    public bool isDead;
    [Tooltip("是否显示攻击和寻敌范围")]
    public bool drawTheBorderOrNot;
    [Tooltip("是否正在攻击")]
    public bool isAttacking;
    public int targetPointIndex { get; private set; } = 0;
    public List<GameObject> playerDetects;
    public List<GameObject> attackDetects;
    [Tooltip("巡逻点")]
    public Transform[] patrolPoints;
    public Transform target { get; private set; }
    public Transform cloestPlayer;
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
        AttackLogic();
        if (isDead)
        {
            StartCoroutine(DeadDestroy(timer));
            return;
        }
        if (playerDetects.Count == 1)
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
    public override void DamageEffect()
    {
        base.DamageEffect();
    }
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
        float distance = Mathf.Infinity;
        for (int i = 0; i < playerDetects.Count; i++)
        {
            if (distance > Vector3.Distance(playerDetects[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(playerDetects[i].transform.position, transform.position);
                cloestPlayer = playerDetects[i].transform;
            }
        }
    }
    public virtual void AnimationArcherAttack()
    {

    }
    public virtual void AnimationCasterAttack()
    {

    }
}
