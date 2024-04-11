public class EnemyCasterAttackState : EnemyCasterGroundState
{
    public EnemyCasterAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Caster enemy_Caster) : base(enemy, stateMachine, animboolName, enemy_Caster)
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
        if (enemy_Caster.attackDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Caster.casterIdleState);
    }
}
