using UnityEngine;

public class EnemyTwoHandedSaberChaseState : EnemyTwoHandedSaberGroundState
{
    private Vector3 target;

    public EnemyTwoHandedSaberChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_TwoHandedSaber enemy_TwoHandedSaber) : base(enemy, stateMachine, animboolName, enemy_TwoHandedSaber)
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
        if (enemy_TwoHandedSaber.playerDetects.Count <= 0)
            stateMachine.ChangeState(enemy_TwoHandedSaber.twoHandedSaberIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
