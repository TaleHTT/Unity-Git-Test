public class EnemySaberIdleState : EnemyState
{
    public Enemy_Saber enemy_Saber {  get; set; }
    public EnemySaberIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Saber enemy_Saber) : base(enemy, stateMachine, animBoolName)
    {
        this.enemy_Saber = enemy_Saber;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0, 0);
        stateTimer = 1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(enemy_Saber.saberPatrolState);
    }
}
