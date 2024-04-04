using UnityEngine;
using UnityEngine.Pool;

public class Enemy_Archer : EnemyBase
{
    private ObjectPool<GameObject> pool;
    [Tooltip("¼ýÊ¸Ô¤ÉèÌå")]
    public GameObject arrowPerfab;
    public EnemyArcherIdleState archerIdleState { get; private set; }
    public EnemyArcherPatrolState archerPatrolState { get; private set; }
    public EnemyArcherChaseState archerChaseState { get; private set; }
    public EnemyArcherDeadState archerDeadState { get; private set; }
    public EnemyArcherAttackState archerAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
        archerIdleState = new EnemyArcherIdleState(this, stateMachine, "Idle", this);
        archerPatrolState = new EnemyArcherPatrolState(this, stateMachine, "Move", this);
        archerChaseState = new EnemyArcherChaseState(this, stateMachine, "Move", this);
        archerDeadState = new EnemyArcherDeadState(this, stateMachine, "Dead", this);
        archerAttackState = new EnemyArcherAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(archerIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(archerDeadState);
    }
    public override void playerDetect()
    {
        base.playerDetect();
        if (playerDetects.Count > 0)
        {
            stateMachine.ChangeState(archerChaseState);
        }
    }
    public override void AnimationArcherAttack()
    {
        base.AnimationArcherAttack();
        pool.Get();
    }
    private GameObject createFunc()
    {
        var orb = Instantiate(arrowPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<Orb_Controller>().pool = pool;
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
