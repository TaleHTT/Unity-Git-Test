public class EnemyAssassinAttackState : EnemyAssassinGroundState
{
    public EnemyAssassinAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Assassin enemy_Assassin) : base(enemy, stateMachine, animboolName, enemy_Assassin)
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
            stateMachine.ChangeState(enemy_Assassin.assassinIdleState);
    }
}
