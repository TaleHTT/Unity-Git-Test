using UnityEngine;

public class EnemySaberChaseState : EnemySaberGroundState
{
    private Vector3 target;
    public EnemySaberChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Saber enemy_Saber) : base(enemy, stateMachine, animboolName, enemy_Saber)
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
        if (enemy_Saber.playerDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Saber.saberIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
