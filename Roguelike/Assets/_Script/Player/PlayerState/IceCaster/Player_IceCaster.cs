using UnityEngine;
using UnityEngine.Pool;

public class Player_IceCaster : PlayerBase
{
    private ObjectPool<GameObject> orbPool;
    [Tooltip("法球预制体")]
    public GameObject orbPerfab;
    public PlayerIceCasterAttackState iceCasterAttackState {  get; set; }
    public PlayerIceCasterDeadState iceCasterDeadState { get; set; }
    public PlayerIceCasterIdleState iceCasterIdleState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        orbPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        iceCasterIdleState = new PlayerIceCasterIdleState(this, stateMachine, "Idle", this);
        iceCasterDeadState = new PlayerIceCasterDeadState(this, stateMachine, "Dead", this);
        iceCasterAttackState = new PlayerIceCasterAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.ChangeState(iceCasterIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(iceCasterDeadState);
    }
    public override void AnimationIceCasterAttack()
    {
        base.AnimationIceCasterAttack();
        orbPool.Get();
    }
    private GameObject CreateFunc()
    {
        var orb = Instantiate(orbPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<Orb_Controller>().orbPool = orbPool;
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