public class Boss_2_Skill_4_State : BossState
{
    private Boss_2 boss_2;
    public Boss_2_Skill_4_State(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss_2 boss_2) : base(boss, stateMachine, animboolName)
    {
        this.boss_2 = boss_2;
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
