public class EnemyArcherAttackState : EnemyArcherChaseState
{
    public EnemyArcherAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName, enemy_Archer)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.isAttacking = true;
        enemy.SetVelocity(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.isAttacking = false;
    }

    public override void Update()
    {
        base.Update();
    }
}
