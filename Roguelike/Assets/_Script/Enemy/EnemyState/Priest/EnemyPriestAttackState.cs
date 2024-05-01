public class EnemyPriestAttackState : EnemyPriestGroundState
{
    public EnemyPriestAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Priest enemy_Priest) : base(enemy, stateMachine, animboolName, enemy_Priest)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.anim.speed = enemy.stats.attackSpeed.GetValue() + defaultAttackSpeed;
        if (triggerCalled)
            stateMachine.ChangeState(enemy_Priest.priestIdleState);
    }
}
