public class EnemyTwoHandedSaberIdleState : EnemyTwoHandedSaberGroundState
{
    public EnemyTwoHandedSaberIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_TwoHandedSaber enemy_TwoHandedSaber) : base(enemy, stateMachine, animboolName, enemy_TwoHandedSaber)
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
            stateMachine.ChangeState(enemy_TwoHandedSaber.twoHandedSaberPatrolState);
    }
}
