using Pathfinding;
using UnityEngine;

public class Player_Assassin : PlayerBase
{
    public bool isTest;
    
    [HideInInspector] public GameObject target;
    [HideInInspector] public bool isStrengthen;
    public Seeker seeker {  get; set; }
    public float durationTimer {  get; set; }
    public GameObject assassinateTarget {  get; set; }
    public PlayerAssassinIdleState assassinIdleState { get; set; }
    public PlayerAssassinDeadState assassinDeadState { get; set; }
    public PlayerAssassinMoveState assassinMoveState { get; set; }
    public PlayerAssassinAttackState assassinAttackState {  get; set; }
    public Assassin_Skill_Controller assassin_Skill_Controller { get; set; }
    public PlayerAssassinStealthIdleState assassinStealthIdleState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        seeker = GetComponent<Seeker>();
        assassin_Skill_Controller = GetComponent<Assassin_Skill_Controller>();
        assassinIdleState = new PlayerAssassinIdleState(this, stateMachine, "Idle", this);
        assassinMoveState = new PlayerAssassinMoveState(this, stateMachine, "Move", this);
        assassinDeadState = new PlayerAssassinDeadState(this, stateMachine, "Dead", this);
        assassinAttackState = new PlayerAssassinAttackState(this, stateMachine, "Attack", this);
        assassinStealthIdleState = new PlayerAssassinStealthIdleState(this, stateMachine, "StealthIdle", this);
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
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(assassinDeadState);
        AssassinateTarget();
        if(target != null)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > attackRadius)
                target = null;
        }
    }
    public void AssassinateTarget()
    {
        float hp = Mathf.Infinity;
        for(int i = 0; i < enemyDetects.Count; i++)
        {
            if(hp >= enemyDetects[i].GetComponent<EnemyBase>().stats.currentHealth)
            {
                hp = enemyDetects[i].GetComponent<EnemyBase>().stats.currentHealth;
                assassinateTarget = enemyDetects[i];
            }
        }
    }
    public void DeadDetect()
    {
        if (target != null)
        {
            if (isStrengthen)
            {
                if (target.GetComponent<EnemyBase>().isDead)
                {
                    if (SkillManger.instance.assassin_Skill.isHave_X_Equipment)
                    {
                        stats.TakeTreat(stats.damage.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddHp);
                        //assassin_Skill_Controller.num_KillEnemy++;
                        Debug.Log("20");
                    }
                    stateMachine.ChangeState(assassinStealthIdleState);
                    target = null;
                }
            }
            else if (isStrengthen == false)
            {
                if (SkillManger.instance.assassin_Skill.isHave_X_Equipment)
                {
                    if (target.GetComponent<EnemyBase>().isDead)
                    {
                        if (target.GetComponent<EnemyBase>().isHunting)
                            stateMachine.ChangeState(assassinStealthIdleState);
                        stats.TakeTreat(stats.damage.GetValue() * DataManager.instance.assassin_Skill_Data.extraAddHp);
                        //assassin_Skill_Controller.num_KillEnemy++;
                        Debug.Log("10");
                        target = null;
                    }
                }
            }
        }
    }
}