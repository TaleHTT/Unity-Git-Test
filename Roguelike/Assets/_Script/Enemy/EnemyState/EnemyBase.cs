using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyOccupation
{
    Saber,
    Archer,
    Caaster
}
public class EnemyBase : Base
{
    public EnemyOccupation occupation;

    public int layersOfBurning;
    public LayerMask whatIsPlayer {  get; set; }

    [Header("Chase info")]
    [Tooltip("索敌范围")]
    public float chaseRadius;
    public float attackRadius;

    [Tooltip("死亡后，经过timer秒后销毁物体")]
    [SerializeField] public float timer {  get; set; }

    [Tooltip("判断是否死亡")]
    [SerializeField] public bool isDead {  get; set; }

    [Tooltip("是否显示攻击和寻敌范围")]
    [SerializeField] public bool drawTheBorderOrNot {  get; set; }

    [Tooltip("是否正在攻击")]
    public bool isAttacking {  get; set; }
    public List<GameObject> playerDetects {  get; set; }
    public List<GameObject> attackDetects { get; set; }

    [Tooltip("巡逻点")]
    public Transform[] patrolPoints {  get; set; }
    public Transform cloestTarget { get; set; }
    public Seeker seeker { get; private set; }
    public EnemyStateMachine stateMachine { get; set; }
    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();
        playerDetects = new List<GameObject>();
        attackDetects = new List<GameObject>();
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
            return;
        }
        playerDetect();
        AttackDetect();
        CloestTargetDetect();
    }
    public void playerDetect()
    {
        detectTimer -= Time.deltaTime;
        if (detectTimer > 0)
        {
            detectTimer = 1;
            return;
        }
        var colliders = Physics2D.OverlapCircleAll(transform.position, chaseRadius, whatIsPlayer);
        foreach (var player in colliders)
        {
            playerDetects.Add(player.gameObject);
        }
    }
    public void CloestTargetDetect()
    {
        float distance = Mathf.Infinity;
        for (int i = 0; i < playerDetects.Count; i++)
        {
            if (distance > Vector3.Distance(playerDetects[i].transform.position, transform.position))
            {
                distance = Vector3.Distance(playerDetects[i].transform.position, transform.position);
                cloestTarget = playerDetects[i].transform;
            }
        }
    }
    public void AttackDetect()
    {
        detectTimer -= Time.deltaTime;
        if (detectTimer < 0)
        {
            detectTimer = 1;
            return;
        }
        var colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsPlayer);
        foreach (var player in colliders)
        {
            attackDetects.Add(player.gameObject);
        }
    }
    public void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
    public IEnumerator DeadDestroy(float timer)
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
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
