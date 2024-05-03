
using UnityEngine;

public class Boss_1_Skill_2_State : BossState
{
    private Boss_1 boss_1;
    public Boss_1_Skill_2_State(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss_1 boss_1) : base(boss, stateMachine, animboolName)
    {
        this.boss_1 = boss_1;
    }

    public override void Enter()
    {
        base.Enter();
        boss_1.num++;
        boss_1.pool.Get();
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
            stateMachine.ChangeState(boss_1.idleState);
    }
}
