using UnityEngine;

public class Enemy_Saber : EnemyBase
{
    private float stadnTimer;
    [HideInInspector] public bool isDefense;
    public EnemySaberIdleState saberIdleState { get; private set; }
    public EnemySaberDeadState saberDeadState { get; private set; }
    public EnemySaberChaseState saberChaseState { get; private set; }
    public Saber_Skill_Controller saber_Skill_Controller { get; set; }
    public EnemySaberAttackState saberAttackState { get; private set; }
    public EnemySaberPatrolState saberPatrolState { get; private set; }
    public EnemySaberDefenseState saberDefenseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        saber_Skill_Controller = GetComponent<Saber_Skill_Controller>();
        saberDeadState = new EnemySaberDeadState(this, stateMachine, "Dead", this);
        saberIdleState = new EnemySaberIdleState(this, stateMachine, "Idle", this);
        saberChaseState = new EnemySaberChaseState(this, stateMachine, "Move", this);
        saberPatrolState = new EnemySaberPatrolState(this, stateMachine, "Move", this);
        saberAttackState = new EnemySaberAttackState(this, stateMachine, "Attack", this);
        saberDefenseState = new EnemySaberDefenseState(this, stateMachine, "Defense", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(saberIdleState);
        stadnTimer = DataManager.instance.saber_Skill_Data.standTimer;
    }
    protected override void Update()
    {
        base.Update();
        if (isHit == true)
        {
            saber_Skill_Controller.numOfHit++;
            isHit = false;
        }
        if (isDefense == true)
        {
            stadnTimer = DataManager.instance.saber_Skill_Data.standTimer;
            return;
        }
        else if (isDefense == false)
        {
            stadnTimer -= Time.deltaTime;
            if (stadnTimer <= 0)
                stateMachine.ChangeState(saberDefenseState);
        }
    }
}
