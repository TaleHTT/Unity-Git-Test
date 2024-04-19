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
    [Tooltip("Ë÷µÐ·¶Î§")]
    public float chaseRadius;
    public float attackRadius;

    [Tooltip("ÊÇ·ñÏÔÊ¾¹¥»÷ºÍÑ°µÐ·¶Î§")]
    [SerializeField] public bool drawTheBorderOrNot {  get; set; }

    [Tooltip("ÊÇ·ñÕýÔÚ¹¥»÷")]
    public bool isAttacking {  get; set; }
    public List<GameObject> playerDetects {  get; set; }
    public List<GameObject> attackDetects { get; set; }

    [Tooltip("Ñ²Âßµã")]
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
            if(layersOfBurning > 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.radius);
                foreach(var hit in colliders)
                {
                    if(hit.GetComponent<EnemyBase>() != null)
                    {
                        hit.GetComponent<EnemyBase>().layersOfBurning++;
                        hit.GetComponent<EnemyStats>().AuthenticTakeDamage(stats.damage.GetValue() * (1 + DataManager.instance.caster_Skill_Data.extraAddDamage) * layersOfBurning);
                    }
                }
            }
            StartCoroutine(DeadDestroy(deadTimer));
            return;
        }
        playerDetect();
        AttackDetect();
        CloestTargetDetect();
    }
    public void playerDetect()
    {
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
