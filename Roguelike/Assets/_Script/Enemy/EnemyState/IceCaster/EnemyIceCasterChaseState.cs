using UnityEngine;

public class EnemyIceCasterChaseState : EnemyIceCasterGroundState
{
    private Vector3 target;

    public EnemyIceCasterChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_IceCaster enemy_IceCaster) : base(enemy, stateMachine, animboolName, enemy_IceCaster)
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
            stateMachine.ChangeState(enemy_IceCaster.iceCasterIdleState);
        if (enemy.isAttacking == true)
            return;
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
