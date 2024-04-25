public class BossDeadState : BossState
{
    private Boss boss_1;
    public BossDeadState(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss boss_1) : base(boss, stateMachine, animboolName)
    {
        this.boss_1 = boss_1;
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
    }
}