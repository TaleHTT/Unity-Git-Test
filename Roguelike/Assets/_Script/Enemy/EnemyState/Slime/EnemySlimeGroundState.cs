public class EnemySlimeGroundState : EnemyState
{
    public Enemy_Slime enemy_Slime;
    public EnemySlimeGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Slime enemy_Slime) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Slime = enemy_Slime;
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
        if (enemy_Slime.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Slime.slimeChaseState);
        if (enemy_Slime.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Slime.slimeAttackState);
        if (enemy_Slime.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_Slime.slimeDeadState);
    }
}