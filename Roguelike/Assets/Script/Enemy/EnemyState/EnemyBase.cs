using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : Entity
{
    [Header("Chase info")]
    public float chaseRadius;

    public int targetPointIndex = 0;
    public List<GameObject> playerDetects;
    public List<GameObject> attackDetects;
    public Transform[] patrolPoints;
    public Transform target;
    public Seeker seeker;
    public EnemyStateMachine stateMachine { get; private set; }



    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

    }
    protected override void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        if(playerDetects.Count == 1)
        {
            target = playerDetects[0].transform;
        }
        else
        {
            AttackLogic();
        }
    }
    public void DamageEffect()
    {
        Debug.Log("I am damage");
    }
    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stats.attackRadius.GetValue());
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
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
    public virtual void AnimationArcherAttack()
    {
        
    }
    private void AttackLogic()
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
}
