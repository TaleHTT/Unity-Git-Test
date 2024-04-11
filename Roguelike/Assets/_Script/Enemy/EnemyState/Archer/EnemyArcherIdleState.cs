public class EnemyArcherIdleState : EnemyArcherGroundState
{
    public EnemyArcherIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName, enemy_Archer)
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
            stateMachine.ChangeState(enemy_Archer.archerPatrolState);
    }
}
