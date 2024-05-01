public class EnemyPriestIdleState : EnemyPriestGroundState
{
    public EnemyPriestIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Priest enemy_Priest) : base(enemy, stateMachine, animboolName, enemy_Priest)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
            stateMachine.ChangeState(enemy_Priest.priestPatrolState);
    }
}
