public class EnemyCasterGroundState : EnemyState
{
    public Enemy_Caster enemy_Caster;
    public EnemyCasterGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Caster enemy_Caster) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Caster = enemy_Caster;
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
        if (enemy_Caster.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Caster.casterChaseState);
        if (enemy_Caster.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Caster.casterAttackState);
        if (enemy_Caster.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_Caster.casterDeadState);
    }
}
