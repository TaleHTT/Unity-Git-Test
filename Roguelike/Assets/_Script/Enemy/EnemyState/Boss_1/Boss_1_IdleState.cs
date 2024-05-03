public class Boss_1_IdleState : BossState
{
    private Boss_1 boss_1;
    public Boss_1_IdleState(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss_1 boss_1) : base(boss, stateMachine, animboolName)
    {
        this.boss_1 = boss_1;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            if (boss_1.num == 2)
                stateMachine.ChangeState(boss_1.skill_2_State);
            if (boss_1.num == 3 || boss_1.num == 6)
                stateMachine.ChangeState(boss_1.skill_1_State);
            if (boss_1.num == 5)
                stateMachine.ChangeState(boss_1.skill_3_State);
        }
    }
}