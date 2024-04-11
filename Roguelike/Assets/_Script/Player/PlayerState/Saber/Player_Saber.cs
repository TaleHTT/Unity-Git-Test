using UnityEngine;

public class Player_Saber : PlayerBase
{
    public bool isDefense {  get; set; }
    public PlayerSaberIdleState saberIdleState { get; private set; }
    public PlayerSaberDeadState saberDeadState { get; private set; }
    public PlayerSaberAttackState saberAttackState { get; private set; }
    public PlayerSaberDefenseState saberDefenseState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
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
        if (stats.currentHealth <= 0 && isDead == false)
            stateMachine.ChangeState(saberDeadState);
        if (isDefense == false && Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(saberDefenseState);
    }
}
