public class EnemyIceCasterAttackState : EnemyIceCasterGroundState
{
    public EnemyIceCasterAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_IceCaster enemy_IceCaster) : base(enemy, stateMachine, animboolName, enemy_IceCaster)
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
        enemy.anim.speed = enemy.stats.attackSpeed.GetValue();
        if (triggerCalled)
            stateMachine.ChangeState(enemy_IceCaster.iceCasterIdleState);
    }
}
