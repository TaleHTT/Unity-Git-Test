using UnityEngine;
using UnityEngine.Pool;

public class Player_Caster : PlayerBase
{
    [Tooltip("∑®«Ú‘§÷∆ÃÂ")]
    public GameObject orbPerfab;
    
    public ObjectPool<GameObject> orbPool;
    
    private Player_Caster_Skill_Controller player_Caster_Skill_Controller;
    public PlayerCasterIdleState casterIdleState { get; private set; }
    public PlayerCasterDeadState casterDeadState { get; private set; }
    public PlayerCasterAttackState casterAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        player_Caster_Skill_Controller = GetComponent<Player_Caster_Skill_Controller>();
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
        player_Caster_Skill_Controller.numberOfAttack++;
        orbPool.Get();
    }
    private GameObject CreateFunc()
    {
        var orb = Instantiate(orbPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<Player_Orb_Controller>().player_Caster_Skill_Controller = player_Caster_Skill_Controller;
        orb.GetComponent<Player_Orb_Controller>().orbPool = orbPool;
        orb.GetComponent<Player_Orb_Controller>().damage = stats.damage.GetValue();
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
