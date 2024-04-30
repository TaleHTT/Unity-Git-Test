using Pathfinding;
using UnityEngine;

public class Enemy_Assassin : EnemyBase
{
    float value;
    public float durationTimer { get; set; }
    public float timer {  get; set; }

    [HideInInspector] public GameObject target;
    [HideInInspector] public bool isStrengthen;
    public Enemy_Assassin_Skill_Controller enemy_Assassin_Skill_Controller { get; set; }
    public GameObject assassinateTarget { get; set; }
    public EnemyAssassinIdleState assassinIdleState { get; private set; }
    public EnemyAssassinPatrolState assassinPatrolState { get; private set; }
    public EnemyAssassinChaseState assassinChaseState { get; private set; }
    public EnemyAssassinDeadState assassinDeadState { get; private set; }
    public EnemyAssassinAttackState assassinAttackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        timer = 3f;
        enemy_Assassin_Skill_Controller = GetComponent<Enemy_Assassin_Skill_Controller>();
        assassinIdleState = new EnemyAssassinIdleState(this, stateMachine, "Idle", this);
        assassinPatrolState = new EnemyAssassinPatrolState(this, stateMachine, "Move", this);
        assassinChaseState = new EnemyAssassinChaseState(this, stateMachine, "Move", this);
        assassinDeadState = new EnemyAssassinDeadState(this, stateMachine, "Dead", this);
        assassinAttackState = new EnemyAssassinAttackState(this, stateMachine, "Attack", this);
    }
    protected override void Start()
    {
        base.Start();
        durationTimer = DataManager.instance.assassin_Skill_Data.skill_1_durationTimer;
        stateMachine.Initialize(assassinIdleState);
    }
    protected override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            value = stats.moveSpeed.GetValue();
            isStrengthen = true;
            isStealth = true;
            durationTimer = DataManager.instance.assassin_Skill_Data.skill_1_durationTimer;
            stats.moveSpeed.baseValue += value * DataManager.instance.assassin_Skill_Data.extraMoveSpeed;
            if(isStealth == false)
            {
                stats.moveSpeed.baseValue -= value * DataManager.instance.assassin_Skill_Data.extraMoveSpeed;
                timer = 3f;
            }
        }
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(assassinDeadState);
        AssassinateTarget();
        if (target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > attackRadius)
                target = null;
        }
    }
    public void AssassinateTarget()
    {
        float hp = Mathf.Infinity;
        for (int i = 0; i < playerDetects.Count; i++)
        {
            if (hp >= playerDetects[i].GetComponent<PlayerBase>().stats.currentHealth)
            {
                hp = playerDetects[i].GetComponent<PlayerBase>().stats.currentHealth;
                assassinateTarget = playerDetects[i];
            }
        }
    }
}
