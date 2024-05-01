public class EnemyBloodsuckerGroundState : EnemyState
{
    public Enemy_Bloodsucker enemy_Bloodsucker;

    public EnemyBloodsuckerGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Bloodsucker enemy_Bloodsucker) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Bloodsucker = enemy_Bloodsucker;
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
        if (enemy_Bloodsucker.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Bloodsucker.bloodsuckerChaseState);
        if (enemy.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Bloodsucker.bloodsuckerAttackState);
        if (enemy_Bloodsucker.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_Bloodsucker.bloodsuckerDeadState);
    }
}
