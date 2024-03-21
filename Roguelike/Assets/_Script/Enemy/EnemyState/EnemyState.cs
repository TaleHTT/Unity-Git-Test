using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    private float pathGenerateInterval;//º‰∏Ù ±º‰
    private float pathGenerateTimer;
    public int currentIndex;
    public int targetPointIndex = 0;
    public List<Vector3> pathPointList;
    public EnemyBase enemy { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public float distance;
    public float stateTimer;
    public bool triggerCalled;
    string animBoolName;

    public EnemyState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animboolName;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        enemy.playerDetect();
    }
    public virtual void Enter()
    {
        enemy.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }
    public void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(enemy.target.transform.position);
            pathGenerateTimer = 0;
        }
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(enemy.target.transform.position);
        }
        else if (Vector2.Distance(enemy.transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(enemy.target.transform.position);
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
}
