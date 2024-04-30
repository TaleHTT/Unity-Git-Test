using UnityEngine;

public class EnemyBloodsuckerChaseState : EnemyBloodsuckerGroundState
{
    private Vector3 target;

    public EnemyBloodsuckerChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Bloodsucker enemy_Bloodsucker) : base(enemy, stateMachine, animboolName, enemy_Bloodsucker)
    {
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
        if (enemy.playerDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Bloodsucker.bloodsuckerIdleState);
        if (enemy.isAttacking == true)
            return;
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
