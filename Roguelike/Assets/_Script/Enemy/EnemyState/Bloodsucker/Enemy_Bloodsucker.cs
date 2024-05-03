using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Bloodsucker : EnemyBase
{
    public int position;
    public GameObject batPrefab;
    ObjectPool<GameObject> batPool;
    public float meleeAttackRadius;
    public float remoteAttackRadius;
    public EnemyBloodsuckerIdleState bloodsuckerIdleState { get; private set; }
    public EnemyBloodsuckerPatrolState bloodsuckerPatrolState { get; private set; }
    public EnemyBloodsuckerChaseState bloodsuckerChaseState { get; private set; }
    public EnemyBloodsuckerDeadState bloodsuckerDeadState { get; private set; }
    public EnemyBloodsuckerAttackState bloodsuckerAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        position = Random.Range(0, 7);
        batPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        bloodsuckerIdleState = new EnemyBloodsuckerIdleState(this, stateMachine, "Idle", this);
        bloodsuckerPatrolState = new EnemyBloodsuckerPatrolState(this, stateMachine, "Move", this);
        bloodsuckerChaseState = new EnemyBloodsuckerChaseState(this, stateMachine, "Move", this);
        bloodsuckerDeadState = new EnemyBloodsuckerDeadState(this, stateMachine, "Dead", this);
        bloodsuckerAttackState = new EnemyBloodsuckerAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(bloodsuckerIdleState);
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void AnimationBloodsuckerAttack()
    {
        base.AnimationBloodsuckerAttack();
        batPool.Get();
    }
    private GameObject CreateFunc()
    {
        var bat = Instantiate(batPrefab, transform.position, Quaternion.identity);
        bat.GetComponent<Enemy_Bat_Controller>().enemy_Bloodsucker = this;
        bat.GetComponent<Enemy_Bat_Controller>().batPool = batPool;
        bat.GetComponent<Enemy_Bat_Controller>().damage = stats.damage.GetValue();
        return bat;
    }
    private void ActionOnGet(GameObject bat)
    {
        bat.transform.position = transform.position;
        bat.SetActive(true);
    }
    private void ActionOnRelease(GameObject bat)
    {
        bat.SetActive(false);
    }
    private void ActionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
    public override void OnDrawGizmos()
    {
        if (!drawTheBorderOrNot)
            return;
        if (position == 0 || position == 1 || position == 2)
        {
            Gizmos.DrawWireSphere(transform.position, meleeAttackRadius);
        }
        else if (position == 3 || position == 4 || position == 5)
        {
            Gizmos.DrawWireSphere(transform.position, remoteAttackRadius);
        }
    }
    public override void PlayerDetect()
    {
        playerDetects = new List<GameObject>();
        if (position == 0 || position == 1 || position == 2)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, meleeAttackRadius, whatIsPlayer);
            foreach (var enemy in colliders)
            {
                playerDetects.Add(enemy.gameObject);
            }
        }
        else
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, remoteAttackRadius, whatIsPlayer);
            foreach (var enemy in colliders)
            {
                playerDetects.Add(enemy.gameObject);
            }
        }
    }
}
