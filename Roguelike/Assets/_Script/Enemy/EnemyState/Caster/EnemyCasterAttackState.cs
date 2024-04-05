public class EnemyCasterAttackState : EnemyCasterChaseState
{
    public EnemyCasterAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Caster enemy_Caster) : base(enemy, stateMachine, animboolName, enemy_Caster)
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
