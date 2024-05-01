public class EnemyIceCasterIdleState : EnemyIceCasterGroundState
{
    public EnemyIceCasterIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_IceCaster enemy_IceCaster) : base(enemy, stateMachine, animboolName, enemy_IceCaster)
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
            stateMachine.ChangeState(enemy_IceCaster.iceCasterPatrolState);
    }
}
