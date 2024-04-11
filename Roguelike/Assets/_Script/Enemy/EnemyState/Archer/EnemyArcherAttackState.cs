public class EnemyArcherAttackState : EnemyArcherGroundState
{
    public EnemyArcherAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName, enemy_Archer)
    {

    }
    public override void Enter()
    {
        base.Enter();
        enemy.isAttacking = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isAttacking = false;
    }

    public override void Update()
    {
        base.Update();
        if (enemy_Archer.attackDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Archer.archerIdleState);
    }
}
