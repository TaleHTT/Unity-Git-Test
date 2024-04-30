public class EnemyAssassinIdleState : EnemyAssassinGroundState
{
    public EnemyAssassinIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Assassin enemy_Assassin) : base(enemy, stateMachine, animboolName, enemy_Assassin)
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
            stateMachine.ChangeState(enemy_Assassin.assassinPatrolState);
    }
}
