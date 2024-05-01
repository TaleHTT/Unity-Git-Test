public class Boss_2_IdleState : BossState
{
    private Boss_2 boss_2;
    public Boss_2_IdleState(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss_2 boss_2) : base(boss, stateMachine, animboolName)
    {
        this.boss_2 = boss_2;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(boss_2.skill_1_State);
    }
}
