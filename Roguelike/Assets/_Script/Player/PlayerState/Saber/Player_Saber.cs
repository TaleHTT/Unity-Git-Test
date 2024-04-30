using UnityEngine;

public class Player_Saber : PlayerBase
{
    private float stadnTimer;
    [HideInInspector] public bool isMove;
    [HideInInspector] public bool isDefense;

    string Player_SaberPrefabPath = "PlayerPrefab/PlayerSaber";

    public override string prefabPath { get { return Player_SaberPrefabPath; } }

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
        stadnTimer = DataManager.instance.saber_Skill_Data.standTimer;
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0))
            isMove = true;
        else
            isMove = false;


        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(saberDeadState);

        if (isHit == true)
        {
            saber_Skill_Controller.numOfHit++;
            isHit = false;
        }
        if (isMove == false)
        {
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
}
