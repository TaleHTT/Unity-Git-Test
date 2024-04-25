
using UnityEngine;

public class BossSkill_2_State : BossState
{
    private Boss boss_1;
    public BossSkill_2_State(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss boss_1) : base(boss, stateMachine, animboolName)
    {
        this.boss_1 = boss_1;
    }

    public override void Enter()
    {
        base.Enter();
        boss_1.num++;
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
        {
            int a = Random.Range(0, 2);
            if (a == 0)
            {
                boss_1.angle = 0;
                boss_1.pool.Get();
            }
            else if (a == 1)
            {
                boss_1.angle = 180;
                boss_1.pool.Get();
            }
            stateMachine.ChangeState(boss_1.idleState);
        }
    }
}
