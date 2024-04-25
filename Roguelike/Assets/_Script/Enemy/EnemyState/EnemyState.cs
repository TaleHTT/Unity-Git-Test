using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : IEnemy
{
    private float pathGenerateInterval;
    private float pathGenerateTimer;
    public int currentIndex;
    public int targetPointIndex = 0;
    public List<Vector3> pathPointList;
    public EnemyBase enemy { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public float distance;
    public float stateTimer;
    string animBoolName;

    public EnemyState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animboolName;
    }
    public virtual void Update()
    {
        if (enemy.isTest)
            return;
        stateTimer -= Time.deltaTime;
    }
    public virtual void Enter()
    {
        enemy.anim.SetBool(animBoolName, true);
    }
    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }
    public void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(enemy.cloestTarget.transform.position);
            pathGenerateTimer = 0;
        }
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(enemy.cloestTarget.transform.position);
        }
        else if (Vector2.Distance(enemy.transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(enemy.cloestTarget.transform.position);
        }
    }
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        enemy.seeker.StartPath(enemy.transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
    public void DeadLogci()
    {
        enemy.attackRadius = 0;
        enemy.chaseRadius = 0;
        enemy.attackDetects.Clear();
        enemy.playerDetects.Clear();
        enemy.isDead = true;
        EnemyManager.instance.enemyCount--;
        enemy.cd.enabled = false;
    }

    public void AnimationFinishTrigger()
    {
        
    }
}
