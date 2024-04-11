using UnityEngine;

public class EnemyCasterChaseState : EnemyCasterGroundState
{
    private Vector3 target;
    public EnemyCasterChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Caster enemy_Caster) : base(enemy, stateMachine, animboolName, enemy_Caster)
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
            stateMachine.ChangeState(enemy_Caster.casterIdleState);
        if (enemy.isAttacking == true)
            return;
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
