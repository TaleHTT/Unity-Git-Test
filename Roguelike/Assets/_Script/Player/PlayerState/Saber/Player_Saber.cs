using UnityEngine;

public class Player_Saber : PlayerBase
{
    public bool isDefense;
    public bool isMove;
    public CenterPointMoveLogic centerPointMoveLogic;
    public Saber_Skill_Controller saber_Skill_Controller { get; set; }
    public PlayerSaberIdleState saberIdleState { get; private set; }
    public PlayerSaberDeadState saberDeadState { get; private set; }
    public PlayerSaberAttackState saberAttackState { get; private set; }
    public PlayerSaberDefenseState saberDefenseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        saber_Skill_Controller = GetComponent<Saber_Skill_Controller>();
        saberIdleState = new PlayerSaberIdleState(this, stateMachine, "Idle", this);
        saberDeadState = new PlayerSaberDeadState(this, stateMachine, "Dead", this);
        saberAttackState = new PlayerSaberAttackState(this, stateMachine, "Attack", this);
        saberDefenseState = new PlayerSaberDefenseState(this, stateMachine, "Defense", this);
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(saberIdleState);
    }
    protected override void Update()
    {
        base.Update();
        if(Input.GetMouseButton(0))
            isMove = true;
        else
            isMove = false;


        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(saberDeadState);
        if (SkillManger.instance.saber_Skill.CanUseSkill() && isMove == false)
            stateMachine.ChangeState(saberDefenseState);
        if (isHit == true)
            saber_Skill_Controller.numOfHit++;
    }
}
