public class EnemyPriestGroundState : EnemyState
{
    public Enemy_Priest enemy_Priest;
    public EnemyPriestGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Priest enemy_Priest) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Priest = enemy_Priest;
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
        if (enemy_Priest.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Priest.priestChaseState);
        if (enemy_Priest.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Priest.priestAttackState);
        if (enemy_Priest.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_Priest.priestDeadState);
    }
}
