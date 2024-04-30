using UnityEngine;

public class EnemyAssassinChaseState : EnemyAssassinGroundState
{
    private Vector3 target;

    public EnemyAssassinChaseState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Assassin enemy_Assassin) : base(enemy, stateMachine, animboolName, enemy_Assassin)
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
            stateMachine.ChangeState(enemy_Assassin.assassinIdleState);
        if (enemy.isAttacking == true)
            return;
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
}
