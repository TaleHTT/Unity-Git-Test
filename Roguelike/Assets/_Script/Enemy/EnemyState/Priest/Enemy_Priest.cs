using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Priest : EnemyBase
{
    [Tooltip("真伤预制体")]
    public GameObject authenticPerfab;

    private ObjectPool<GameObject> authenticPool;
    [HideInInspector] public List<GameObject> enemyDetects;
    public EnemyPriestIdleState priestIdleState { get; private set; }
    public EnemyPriestPatrolState priestPatrolState { get; private set; }
    public EnemyPriestChaseState priestChaseState { get; private set; }
    public EnemyPriestDeadState priestDeadState { get; private set; }
    public EnemyPriestAttackState priestAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        authenticPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        priestIdleState = new EnemyPriestIdleState(this, stateMachine, "Idle", this);
        priestPatrolState = new EnemyPriestPatrolState(this, stateMachine, "Move", this);
        priestChaseState = new EnemyPriestChaseState(this, stateMachine, "Move", this);
        priestDeadState = new EnemyPriestDeadState(this, stateMachine, "Dead", this);
        priestAttackState = new EnemyPriestAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(priestIdleState);

    }
    protected override void Update()
    {
        base.Update();
    }
    public override void AnimationPriestAttack()
    {
        base.AnimationPriestAttack();
        authenticPool.Get();
    }
    private GameObject CreateFunc()
    {
        var orb = Instantiate(authenticPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<Authentic_Controller>().authenticPool = authenticPool;
        return orb;
    }
    private void ActionOnGet(GameObject orb)
    {
        orb.transform.position = transform.position;
        orb.SetActive(true);
    }
    private void ActionOnRelease(GameObject orb)
    {
        orb.SetActive(false);
    }
    private void ActionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}