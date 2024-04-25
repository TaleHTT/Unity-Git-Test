using System.Collections.Generic;
using UnityEngine;

public class BossState : IEnemy
{
    public int currentIndex;
    public int targetPointIndex = 0;
    private float pathGenerateTimer;
    private float pathGenerateInterval;
    public List<Vector3> pathPointList;

    string animBoolName;
    public float stateTimer;
    public bool triggerCalled { get; set; }
    public BossBase boss { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public BossState(BossBase boss, EnemyStateMachine stateMachine, string animboolName)
    {
        this.boss = boss;
        this.stateMachine = stateMachine;
        this.animBoolName = animboolName;
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void Enter()
    {
        boss.anim.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        boss.anim.SetBool(animBoolName, false);
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
    public void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(boss.player.transform.position);
            pathGenerateTimer = 0;
        }
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(boss.player.transform.position);
        }
        else if (Vector2.Distance(boss.transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(boss.player.transform.position);
        }
    }
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        boss.seeker.StartPath(boss.transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
}