using UnityEngine;

public class BossSkill_3_State : BossState
{
    private Boss boss_1;
    private Transform current;
    public BossSkill_3_State(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss boss_1) : base(boss, stateMachine, animboolName)
    {
        this.boss_1 = boss_1;
    }

    public override void Enter()
    {
        base.Enter();
        boss_1.num++;
        current = boss_1.transform;
        stateTimer = 2f;
    }

    public override void Exit()
    {
        base.Exit();
        boss_1.isFinishSkill = false;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            boss_1.transform.position = Vector2.MoveTowards(boss_1.transform.position, boss_1.player.transform.position, boss_1.sprintSpeed * Time.deltaTime);
            if(Vector2.Distance(boss_1.transform.position, current.position) > 6)
                boss_1.isFinishSkill = true;
        }
        if (boss_1.isFinishSkill)
        {
            stateMachine.ChangeState(boss_1.idleState);
        }
    }
}
