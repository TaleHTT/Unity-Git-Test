public class EnemyCasterIdleState : EnemyState
{
    private Enemy_Caster enemy_Caster;
    public EnemyCasterIdleState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Caster enemy_Caster) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Caster = enemy_Caster;
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
            stateMachine.ChangeState(enemy_Caster.casterPatrolState);
    }
}
