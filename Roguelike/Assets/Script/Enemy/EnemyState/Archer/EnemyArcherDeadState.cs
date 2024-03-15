public class EnemyArcherDeadState : EnemyState
{
    public Enemy_Archer enemy_Archer;
    public EnemyArcherDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Archer = enemy_Archer;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.cd.enabled = false;
        enemy.stats.attackRadius.baseValue = 0;
        enemy.chaseRadius = 0;
        enemy.attackDetects.Clear();
        enemy.playerDetects.Clear();
        enemy.isDead = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.SetVelocity(0, 0);
    }
}
