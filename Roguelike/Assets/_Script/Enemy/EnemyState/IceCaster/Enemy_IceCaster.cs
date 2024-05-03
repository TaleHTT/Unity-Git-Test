using UnityEngine;
using UnityEngine.Pool;

public class Enemy_IceCaster : EnemyBase
{
    [Tooltip("法球预制体")]
    public GameObject orbPerfab;

    private ObjectPool<GameObject> orbPool;
    public EnemyIceCasterIdleState iceCasterIdleState { get; private set; }
    public EnemyIceCasterAttackState iceCasterAttackState { get; private set; }
    public EnemyIceCasterDeadState iceCasterDeadState { get; private set; }
    public EnemyIceCasterPatrolState iceCasterPatrolState { get; private set; }
    public EnemyIceCasterChaseState iceCasterChaseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        orbPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        iceCasterIdleState = new EnemyIceCasterIdleState(this, stateMachine, "Idle", this);
        iceCasterAttackState = new EnemyIceCasterAttackState(this, stateMachine, "Attack", this);
        iceCasterDeadState = new EnemyIceCasterDeadState(this, stateMachine, "Dead", this);
        iceCasterPatrolState = new EnemyIceCasterPatrolState(this, stateMachine, "Move", this);
        iceCasterChaseState = new EnemyIceCasterChaseState(this, stateMachine, "Move", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(iceCasterIdleState);
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void AnimationCasterAttack()
    {
        base.AnimationCasterAttack();
        orbPool.Get();
    }
    public override void AnimationIceCasterAttack()
    {
        base.AnimationIceCasterAttack();
        orbPool.Get();
    }
    private GameObject CreateFunc()
    {
        var orb = Instantiate(orbPerfab, transform.position, Quaternion.identity, this.transform);
        orb.GetComponent<Enemy_IceOrb_Controller>().orbPool = orbPool;
        orb.GetComponent<Enemy_IceOrb_Controller>().enemy_IceCaster = this;
        orb.GetComponent<Enemy_IceOrb_Controller>().damage = stats.damage.GetValue();
        return orb;
    }
    private void ActionOnGet(GameObject orb)
    {
        orb.SetActive(true);
        orb.transform.position = transform.position;
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
