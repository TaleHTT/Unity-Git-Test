public class EnemyArcherDeadState : EnemyState
{
    public Enemy_Archer enemy_Archer;
    public EnemyArcherDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Archer = enemy_Archer;
    }

    public override void Enter()
    {
        base.Enter();
        DeadLogci();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy_Archer.isDead == false)
            stateMachine.ChangeState(enemy_Archer.archerIdleState);
    }
}
