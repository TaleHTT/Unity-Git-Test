using UnityEngine;
using UnityEngine.Pool;

public class Player_Caster : PlayerBase
{
    private ObjectPool<GameObject> orbPool;
    [Tooltip("∑®«Ú‘§÷∆ÃÂ")]
    public GameObject orbPerfab;
    private Caster_Skill_Controller caster_Skill_Controller;
    public PlayerCasterIdleState casterIdleState { get; private set; }
    public PlayerCasterAttackState casterAttackState { get; private set; }
    public PlayerCasterDeadState casterDeadState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        caster_Skill_Controller = GetComponent<Caster_Skill_Controller>();
        orbPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
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
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(casterDeadState);
    }
    public override void AnimationCasterAttack()
    {
        base.AnimationCasterAttack();
        caster_Skill_Controller.numberOfAttack++;
        orbPool.Get();
    }
    private GameObject CreateFunc()
    {
        var orb = Instantiate(orbPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<Orb_Controller>().caster_Skill_Controller = caster_Skill_Controller;
        orb.GetComponent<Orb_Controller>().orbPool = orbPool;
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
