using UnityEngine;
using UnityEngine.Pool;

public class Player_IceCaster : PlayerBase
{
    public float timer;
    private ObjectPool<GameObject> orbPool;
    [Tooltip("法球预制体")]
    public GameObject orbPerfab;
    public PlayerIceCasterAttackState iceCasterAttackState {  get; set; }
    public PlayerIceCasterDeadState iceCasterDeadState { get; set; }
    public PlayerIceCasterIdleState iceCasterIdleState { get; set; }
    public PlayerIceCasterSnowstormState iceCasterSnowstorm { get; set; }
    protected override void Awake()
    {
        base.Awake();
        orbPool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestory, true, 10, 1000);
        iceCasterIdleState = new PlayerIceCasterIdleState(this, stateMachine, "Idle", this);
        iceCasterDeadState = new PlayerIceCasterDeadState(this, stateMachine, "Dead", this);
        iceCasterAttackState = new PlayerIceCasterAttackState(this, stateMachine, "Attack", this);
        iceCasterSnowstorm = new PlayerIceCasterSnowstormState(this, stateMachine, "Snowstorm", this);
    }
    protected override void Start()
    {
        base.Start();
        timer = DataManager.instance.iceCasterSkill_Data.skill_X_CD;
        stateMachine.Initialize(iceCasterIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if (SkillManger.instance.iceCaster_Skill.isHave_X_Equipment)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                stateMachine.ChangeState(iceCasterSnowstorm);
        }
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(iceCasterDeadState);
    }
    public void TakeDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DataManager.instance.iceCasterSkill_Data.skill_X_radius, whatIsEnemy);
        foreach(var hit in colliders)
        {
            if(hit.GetComponent<EnemyBase>() != null)
            {
                hit.GetComponent<EnemyStats>().TakeDamage(stats.damage.GetValue() * (1 + DataManager.instance.iceCasterSkill_Data.skill_X_ExtraAddDamage));
                hit.GetComponent<EnemyBase>().layerOfCold++;
            }
        }
    }
    public override void AnimationIceCasterAttack()
    {
        base.AnimationIceCasterAttack();
        orbPool.Get();
    }
    private GameObject CreateFunc()
    {
        var orb = Instantiate(orbPerfab, transform.position, Quaternion.identity);
        orb.GetComponent<IceOrb_Controller>().orbPool = orbPool;
        orb.GetComponent<IceOrb_Controller>().player_IceCaster = this;
        orb.GetComponent<IceOrb_Controller>().damage = stats.damage.GetValue();
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