public class EnemySaberIdleState : EnemySaberGroundState
{
    public EnemySaberIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Saber enemy_Saber) : base(enemy, stateMachine, animBoolName, enemy_Saber)
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
            stateMachine.ChangeState(enemy_Saber.saberPatrolState);
    }
}
