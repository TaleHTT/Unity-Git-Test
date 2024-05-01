using UnityEngine;

public class EnemyIceCasterPatrolState : EnemyIceCasterGroundState
{
    private Vector3 target;

    public EnemyIceCasterPatrolState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_IceCaster enemy_IceCaster) : base(enemy, stateMachine, animboolName, enemy_IceCaster)
    {
    }

    public override void Enter()
    {
        base.Enter();
        GeneratePatrolPoint();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (pathPointList == null || pathPointList.Count <= 0)
        {
            GeneratePatrolPoint();
        }
        else
        {
            target = pathPointList[currentIndex];
            if (Vector2.Distance(enemy.transform.position, pathPointList[currentIndex]) <= .1f)
            {
                currentIndex++;
                if (currentIndex >= pathPointList.Count)
                {
                    stateMachine.ChangeState(enemy_IceCaster.iceCasterIdleState);
                }
                else
                {
                    target = pathPointList[currentIndex];
                }
            }
        }
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, enemy.stats.moveSpeed.GetValue() * Time.deltaTime);
    }
    public void GeneratePatrolPoint()
    {
        while (true)
        {
            int i = Random.Range(0, enemy.patrolPoints.Length);
            if (targetPointIndex != i)
            {
                targetPointIndex = i;
                break;
            }
        }
        GeneratePath(enemy.patrolPoints[targetPointIndex].position);
    }
}
