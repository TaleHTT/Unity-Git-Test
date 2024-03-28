using UnityEngine;
using UnityEngine.Pool;

public class Player_Caster : PlayerBase
{
    private ObjectPool<GameObject> pool;
    [Tooltip("∑®«Ú‘§÷∆ÃÂ")]
    public GameObject OrbPerfab;
    public PlayerCasterIdleState casterIdleState { get; private set; }
    public PlayerCasterAttackState casterAttackState { get; private set; }
    public PlayerCasterDeadState casterDeadState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        pool = new ObjectPool<GameObject>(createFunc, actionOnGet, actionOnRelease, actionOnDestory, true, 10, 1000);
        casterIdleState = new PlayerCasterIdleState(this, stateMachine, "Idle", this);
        casterAttackState = new PlayerCasterAttackState(this, stateMachine, "Attack", this);
        casterDeadState = new PlayerCasterDeadState(this, stateMachine, "Dead", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(casterIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (stats.currentHealth <= 0)
            stateMachine.ChangeState(casterDeadState);
    }
    public override void AnimationCasterAttack()
    {
        base.AnimationCasterAttack();
        pool.Get();
    }
    private GameObject createFunc()
    {
        var orb = Instantiate(OrbPerfab, transform.position, Quaternion.identity);
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
