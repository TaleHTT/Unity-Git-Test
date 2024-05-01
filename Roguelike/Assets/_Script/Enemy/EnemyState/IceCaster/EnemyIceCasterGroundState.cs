public class EnemyIceCasterGroundState : EnemyState
{
    public Enemy_IceCaster enemy_IceCaster;
    public EnemyIceCasterGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_IceCaster enemy_IceCaster) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_IceCaster = enemy_IceCaster;
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
        if (enemy_IceCaster.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_IceCaster.iceCasterChaseState);
        if (enemy_IceCaster.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_IceCaster.iceCasterAttackState);
        if (enemy_IceCaster.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_IceCaster.iceCasterDeadState);
    }
}
