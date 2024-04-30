using UnityEngine;

public class EnemyShamanChaseState : EnemyShamanGroundState
{
    private Vector3 target;
    public EnemyShamanChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Shaman enemy_Shaman) : base(enemy, stateMachine, animboolName, enemy_Shaman)
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
        if (enemy_Shaman.playerDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Shaman.shamanIdleState);
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
