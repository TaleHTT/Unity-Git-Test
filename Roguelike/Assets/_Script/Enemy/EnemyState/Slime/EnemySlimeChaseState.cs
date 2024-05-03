using UnityEngine;

public class EnemySlimeChaseState : EnemySlimeGroundState
{
    private Vector3 target;

    public EnemySlimeChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Slime enemy_Slime) : base(enemy, stateMachine, animboolName, enemy_Slime)
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
        if (enemy_Slime.playerDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Slime.slimeIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
