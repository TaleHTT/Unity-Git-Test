using UnityEngine;

public class EnemyArcherChaseState : EnemyArcherGroundState
{
    public Vector3 target;
    public EnemyArcherChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Archer enemy_Archer) : base(enemy, stateMachine, animboolName, enemy_Archer)
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
            stateMachine.ChangeState(enemy_Archer.archerIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
