public class EnemyBloodsuckerIdleState : EnemyBloodsuckerGroundState
{
    public EnemyBloodsuckerIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Bloodsucker enemy_Bloodsucker) : base(enemy, stateMachine, animboolName, enemy_Bloodsucker)
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
            stateMachine.ChangeState(enemy_Bloodsucker.bloodsuckerPatrolState);
    }
}
