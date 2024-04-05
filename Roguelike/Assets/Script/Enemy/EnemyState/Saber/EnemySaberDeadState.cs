public class EnemySaberDeadState : EnemyState
{
    public Enemy_Saber enemy_Saber;
    public EnemySaberDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Saber enemy_Saber) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Saber = enemy_Saber;
    }

    public override void Enter()
    {
        base.Enter();
        DeadLogci();
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