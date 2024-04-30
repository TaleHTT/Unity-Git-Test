using UnityEngine;

public class EnemyPriestChaseState : EnemyPriestGroundState
{
    private Vector3 target;

    public EnemyPriestChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Priest enemy_Priest) : base(enemy, stateMachine, animboolName, enemy_Priest)
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
        if (enemy_Priest.playerDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Priest.priestIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
