using UnityEngine;

public class Enemy_Summons_Hound_ChaseState : Enemy_Summons_State
{
    private Vector3 target;
    Enemy_Summons_Hound enemy_Summons_Hound;
    public Enemy_Summons_Hound_ChaseState(Enemy_Summons_Base summons_Hound_Base, Enemy_Summons_StateMachine stateMachine, string animBoolName, Enemy_Summons_Hound enemy_Summons_Hound) : base(summons_Hound_Base, stateMachine, animBoolName)
    {
        this.enemy_Summons_Hound = enemy_Summons_Hound;
    }

    public override void Eixt()
    {
        base.Eixt();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (enemy_Summons_Hound.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Summons_Hound.houndAttackState);
        if (enemy_Summons_Hound.playerDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Summons_Hound.houndIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy_Summons_Hound.transform.position = Vector3.MoveTowards(enemy_Summons_Hound.transform.position, target, enemy_Summons_Hound.moveSpeed * Time.deltaTime);
    }
}