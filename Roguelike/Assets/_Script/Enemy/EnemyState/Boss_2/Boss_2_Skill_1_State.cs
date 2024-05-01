using UnityEngine;

public class Boss_2_Skill_1_State : BossState
{
    private Boss_2 boss_2;
    private Vector3 target;
    public Boss_2_Skill_1_State(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss_2 boss_2) : base(boss, stateMachine, animboolName)
    {
        this.boss_2 = boss_2;
    }

    public override void Enter()
    {
        base.Enter();
        if (boss_2.num == 7)
            boss_2.num = 0;
        boss_2.num++;
        stateTimer = 3f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            if (boss_2.num == 1)
                stateMachine.ChangeState(boss_2.skill_2_State);
            else if (boss_2.num == 3)
                stateMachine.ChangeState(boss_2.skill_3_State);
            else if (boss_2.num == 5)
                stateMachine.ChangeState(boss_2.skill_4_State);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, target, boss.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}