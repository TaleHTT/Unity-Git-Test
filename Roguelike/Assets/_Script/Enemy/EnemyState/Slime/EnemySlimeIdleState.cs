public class EnemySlimeIdleState : EnemySlimeGroundState
{
    public EnemySlimeIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Slime enemy_Slime) : base(enemy, stateMachine, animboolName, enemy_Slime)
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
            stateMachine.ChangeState(enemy_Slime.slimePatrolState);
    }
}
