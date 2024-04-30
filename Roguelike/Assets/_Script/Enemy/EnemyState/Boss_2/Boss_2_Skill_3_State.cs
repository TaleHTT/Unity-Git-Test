using UnityEngine;

public class Boss_2_Skill_3_State : BossState
{
    private Boss_2 boss_2;
    float timer = 1f;
    public Boss_2_Skill_3_State(BossBase boss, EnemyStateMachine stateMachine, string animboolName, Boss_2 boss_2) : base(boss, stateMachine, animboolName)
    {
        this.boss_2 = boss_2;
    }

    public override void Enter()
    {
        base.Enter();
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
            stateMachine.ChangeState(boss_2.skill_1_State);
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                for(int i = 0; i < 20; i++)
                {

                }
            }
        }
    }
}
