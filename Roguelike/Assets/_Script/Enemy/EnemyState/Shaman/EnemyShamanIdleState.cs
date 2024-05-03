public class EnemyShamanIdleState : EnemyShamanGroundState
{
    public EnemyShamanIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Shaman enemy_Shaman) : base(enemy, stateMachine, animBoolName, enemy_Shaman)
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
            stateMachine.ChangeState(enemy_Shaman.shamanPatrolState);
    }
}
