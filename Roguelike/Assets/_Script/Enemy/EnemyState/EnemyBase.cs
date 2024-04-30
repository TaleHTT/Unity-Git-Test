using Pathfinding;
using System;
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
    public bool isTest;
    public EnemyOccupation occupation;

    public int layersOfBurning;
    public LayerMask whatIsPlayer;

    [Header("Chase info")]
    [Tooltip("Ë÷µÐ·¶Î§")]
    public float chaseRadius;

    [Tooltip("ÊÇ·ñÏÔÊ¾¹¥»÷ºÍÑ°µÐ·¶Î§")]
    [SerializeField] public bool drawTheBorderOrNot;

    [Tooltip("ÊÇ·ñÕýÔÚ¹¥»÷")]
    public bool isAttacking { get; set; }
    public List<GameObject> playerDetects;
    public List<GameObject> attackDetects;

    [Tooltip("Ñ²Âßµã")]
    public Transform[] patrolPoints;
    public Transform cloestTarget { get; set; }
    public Seeker seeker { get; private set; }
    public EnemyStateMachine stateMachine { get; set; }
    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();
        stateMachine = new EnemyStateMachine();

    }
    protected override void Start()
    {
        base.Start();
        isDead = false;
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
        if (isDead)
        {
            if (layersOfBurning > 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.caster_Skill_Data.radius);
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<EnemyBase>() != null)
                    {
                        hit.GetComponent<EnemyBase>().layersOfBurning = layersOfBurning ;
                        hit.GetComponent<EnemyStats>().AuthenticTakeDamage(stats.damage.GetValue() * (1 + DataManager.instance.caster_Skill_Data.extraAddDamage) * layersOfBurning);
                    }
                }
            }
        }
        if (isDead)
        {
            deadTimer -= Time.deltaTime;
            if(deadTimer < 0)
                gameObject.SetActive(false);
            deadTimer = 3;
            return;
        }
        else
        {
            gameObject.SetActive(true);
        }
        HuntingMark();
        Hound_Bleed();
        Two_Handed_Bleed();
        ColdEffect();
    }
    public void FixedUpdate()
    {
        AttackDetect();
        PlayerDetect();
        CloestTargetDetect();
    }
    public virtual void PlayerDetect()
    {
        playerDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, chaseRadius, whatIsPlayer);
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
        attackDetects = new List<GameObject>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, whatIsPlayer);
        foreach (var player in colliders)
        {
            if(player.GetComponent<PlayerBase>().isStealth == false)
                attackDetects.Add(player.gameObject);
        }
    }
    public virtual void OnDrawGizmos()
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
    public virtual void AnimationPriestAttack()
    {

    }
    public virtual void AnimationBloodsuckerAttack()
    {

    }

    public virtual void AnimationIceCasterAttack()
    {

    }

    public void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
