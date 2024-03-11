public class EnemyArcherIdleState : EnemyState
{
    public Enemy_Archer enemy_Archer;
    public EnemyArcherIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Archer = enemy_Archer;
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
        {
            stateMachine.ChangeState(enemy_Archer.archerMoveState);
        }
    }
}
