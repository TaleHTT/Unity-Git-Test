public class EnemyShamanGroundState : EnemyState
{
    public Enemy_Shaman enemy_Shaman;
    public EnemyShamanGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Shaman enemy_Shaman) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Shaman = enemy_Shaman;
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
        if (enemy_Shaman.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Shaman.shamanChaseState);
        if (enemy_Shaman.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Shaman.shamanAttackState);
        if (enemy_Shaman.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_Shaman.shamanDeadState);
    }
}