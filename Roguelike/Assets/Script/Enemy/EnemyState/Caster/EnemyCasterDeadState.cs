public class EnemyCasterDeadState : EnemyState
{
    public Enemy_Caster enemy_Caster;
    public EnemyCasterDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Caster enemy_Caster) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Caster = enemy_Caster;
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
