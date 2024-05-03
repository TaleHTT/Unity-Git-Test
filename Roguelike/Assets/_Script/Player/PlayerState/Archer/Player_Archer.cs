using UnityEngine;
using UnityEngine.Pool;

public class Player_Archer : PlayerBase
{
    [Tooltip("¼ýÊ¸Ô¤ÖÆÌå")]
    public GameObject arrowPerfab;
    
    private ObjectPool<GameObject> pool;
    public PlayerArcherIdleState archerIdleState { get; private set; }
    public PlayerArcherDeadState archerDeadState { get; private set; }
    public PlayerArcherAttackState archerAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
        archerIdleState = new PlayerArcherIdleState(this, stateMachine, "Idle", this);
        archerDeadState = new PlayerArcherDeadState(this, stateMachine, "Dead", this);
        archerAttackState = new PlayerArcherAttackState(this, stateMachine, "Attack", this);
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
    public override void AnimationArcherAttack()
    {
        base.AnimationArcherAttack();
        pool.Get();
    }
    private GameObject createFunc()
    {
        var orb = Instantiate(arrowPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<Player_Arrow_Controller>().damage = stats.damage.GetValue();
        orb.GetComponent<Player_Arrow_Controller>().pool = pool;
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
