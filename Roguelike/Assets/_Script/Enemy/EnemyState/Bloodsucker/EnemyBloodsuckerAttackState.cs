public class EnemyBloodsuckerAttackState : EnemyBloodsuckerGroundState
{
    public EnemyBloodsuckerAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Bloodsucker enemy_Bloodsucker) : base(enemy, stateMachine, animboolName, enemy_Bloodsucker)
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
            stateMachine.ChangeState(enemy_Bloodsucker.bloodsuckerIdleState);
    }
}
