using UnityEngine;

public class Player_Saber : PlayerBase
{
    public bool isMove {  get; set; }
    public bool isDefense {  get; set; }
    public float coolTimer { get; set; }
    public float standTimer {  get; set; }
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
        standTimer += Time.deltaTime;
        coolTimer += Time.deltaTime;
        if(Input.GetMouseButton(0))
            isMove = true;
        else
            isMove = false;


        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(saberDeadState);

        if (coolTimer >= DataManager.instance.saber_Skill_Data.coolTimer && isMove == false && standTimer >= DataManager.instance.saber_Skill_Data.standTimer)
        {
            stateMachine.ChangeState(saberDefenseState);
            standTimer = 0;
            coolTimer = 0;
        }

        if (isHit == true)
        {
            saber_Skill_Controller.numOfHit++;
            isHit = false;
        }
    }
}
