public class Boss_1_DeadState : BossState
{
    private Boss_1 boss_1;
    public Boss_1_DeadState(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss_1 boss_1) : base(boss, stateMachine, animboolName)
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