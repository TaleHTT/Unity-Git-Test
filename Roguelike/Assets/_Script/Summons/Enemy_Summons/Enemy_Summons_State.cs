using System.Collections.Generic;
using UnityEngine;

public class Enemy_Summons_State
{
    private float pathGenerateInterval;
    private float pathGenerateTimer;
    public int currentIndex;
    public int targetPointIndex = 0;
    public List<Vector3> pathPointList;

    public float distance;
    public float stateTimer;

    public Enemy_Summons_Base enemy_Summons_Base;
    public Enemy_Summons_StateMachine stateMachine;
    private string animBoolName;
    public Enemy_Summons_State(Enemy_Summons_Base summons_Hound_Base, Enemy_Summons_StateMachine stateMachine, string animBoolName)
    {
        this.enemy_Summons_Base = summons_Hound_Base;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Eixt()
    {
        enemy_Summons_Base.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {

    }

    public virtual void Enter()
    {
        enemy_Summons_Base.anim.SetBool(animBoolName, true);
    }
    public void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(enemy_Summons_Base.cloestTarget.transform.position);
            pathGenerateTimer = 0;
        }
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(enemy_Summons_Base.cloestTarget.transform.position);
        }
        else if (Vector2.Distance(enemy_Summons_Base.transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(enemy_Summons_Base.cloestTarget.transform.position);
        }
    }
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        enemy_Summons_Base.seeker.StartPath(enemy_Summons_Base.transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
    public void DeadLogic()
    {
        enemy_Summons_Base.isDead = true;
        enemy_Summons_Base.chaseRadius = 0;
        enemy_Summons_Base.attackRadius = 0;
        enemy_Summons_Base.cd.enabled = false;
        enemy_Summons_Base.attackDetects.Clear();
        enemy_Summons_Base.attackDetects.Clear();
    }
}