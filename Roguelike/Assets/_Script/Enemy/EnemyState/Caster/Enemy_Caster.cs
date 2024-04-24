using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Caster : EnemyBase
{
    private ObjectPool<GameObject> pool;
    [Tooltip("∑®«Ú‘§÷∆ÃÂ")]
    public GameObject OrbPerfab;
    public EnemyCasterIdleState casterIdleState { get; private set; }
    public EnemyCasterAttackState casterAttackState { get; private set; }
    public EnemyCasterDeadState casterDeadState { get; private set; }
    public EnemyCasterPatrolState casterPatrolState { get; private set; }
    public EnemyCasterChaseState casterChaseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
        casterIdleState = new EnemyCasterIdleState(this, stateMachine, "Idle", this);
        casterAttackState = new EnemyCasterAttackState(this, stateMachine, "Attack", this);
        casterDeadState = new EnemyCasterDeadState(this, stateMachine, "Dead", this);
        casterPatrolState = new EnemyCasterPatrolState(this, stateMachine, "Move", this);
        casterChaseState = new EnemyCasterChaseState(this, stateMachine, "Move", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(casterIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(casterDeadState);
    }
    public override void AnimationCasterAttack()
    {
        base.AnimationCasterAttack();
        pool.Get();
    }
    private GameObject createFunc()
    {
        var orb = Instantiate(OrbPerfab, transform.position,Quaternion.identity);
        orb.GetComponent<Orb_Controller>().damage = stats.damage.GetValue();
        orb.GetComponent<Orb_Controller>().orbPool = pool;
        return orb;
    }
    private void actionOnGet(GameObject orb)
    {
        orb.transform.position = transform.position;
        orb.SetActive(true);
    }
    private void actionOnRelease(GameObject orb)
    {
        orb.SetActive(false);
    }
    private void actionOnDestory(GameObject orb)
    {
        Destroy(orb);
    }
}
