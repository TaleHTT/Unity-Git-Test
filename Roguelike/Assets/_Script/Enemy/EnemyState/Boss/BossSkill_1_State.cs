using UnityEngine;

public class BossSkill_1_State : BossState
{
    private Boss boss_1;
    private Vector3 target;
    public BossSkill_1_State(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss boss_1) : base(boss, stateMachine, animboolName)
    {
        this.boss_1 = boss_1;
    }

    public override void Enter()
    {
        base.Enter();
        if (boss_1.num == 7)
            boss_1.num = 0;
        boss_1.num++;
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
            stateMachine.ChangeState(boss_1.skill_3_State);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, target, boss.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}