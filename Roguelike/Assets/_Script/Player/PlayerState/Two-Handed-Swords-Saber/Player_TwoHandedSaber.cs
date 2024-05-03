
using UnityEngine;

public class Player_TwoHandedSaber : PlayerBase
{
    public bool isAttack;
    public float cdTimer;
    public Two_Handed_Saber_Skill_Controller two_Handed_Saber_Skill_Controller { get; set; }
    public PlayerTwoHandedSaberIdleState twoHandedSaberIdleState { get; set; }
    public PlayerTwoHandedSaberDeadState twoHandedSaberDeadState { get; set; }
    public PlayerTwoHandedSaberAttackState twoHandedSaberAttackState { get; set; }
    public PlayerTwoHandedSaberStormBladesState twoHandedSaberStormBladesState { get; set; }
    protected override void Awake()
    {
        base.Awake();
        two_Handed_Saber_Skill_Controller = GetComponent<Two_Handed_Saber_Skill_Controller>();
        twoHandedSaberIdleState = new PlayerTwoHandedSaberIdleState(this, stateMachine, "Idle", this);
        twoHandedSaberDeadState = new PlayerTwoHandedSaberDeadState(this, stateMachine, "Dead", this);
        twoHandedSaberAttackState = new PlayerTwoHandedSaberAttackState(this, stateMachine, "Attack", this);
        twoHandedSaberStormBladesState = new PlayerTwoHandedSaberStormBladesState(this, stateMachine, "Skill", this);
    }
    protected override void Start()
    {
        base.Start();
        cdTimer = DataManager.instance.two_Handed_Saber_Skill_Data.CD;
        stateMachine.Initialize(twoHandedSaberIdleState);
    }
    protected override void Update()
    {
        base.Update();
        cdTimer -= Time.deltaTime;
        if (stats.currentHealth / stats.maxHp.GetValue() < 0.5f && cdTimer <= 0 && stats.isUseSkill == false)
            stateMachine.ChangeState(twoHandedSaberStormBladesState);
    }
}
