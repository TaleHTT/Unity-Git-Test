using System.Collections.Generic;
using UnityEngine;

public class Player_Summons_State
{
    private float pathGenerateInterval;
    private float pathGenerateTimer;
    public int currentIndex;
    public int targetPointIndex = 0;
    public List<Vector3> pathPointList;

    public float distance;
    public float stateTimer;

    public Player_Summons_Base player_Summons_Base;
    public Player_Summons_StateMachine stateMachine;
    private string animBoolName;
    public Player_Summons_State(Player_Summons_Base summons_Hound_Base, Player_Summons_StateMachine stateMachine, string animBoolName)
    {
        this.player_Summons_Base = summons_Hound_Base;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Eixt()
    {
        player_Summons_Base.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {

    }

    public virtual void Enter()
    {
        player_Summons_Base.anim.SetBool(animBoolName, true);
    }
    public void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(player_Summons_Base.chaseTarget.transform.position);
            pathGenerateTimer = 0;
        }
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(player_Summons_Base.chaseTarget.transform.position);
        }
        else if (Vector2.Distance(player_Summons_Base.transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(player_Summons_Base.chaseTarget.transform.position);
        }
    }
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        player_Summons_Base.seeker.StartPath(player_Summons_Base.transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
    public void DeadLogic()
    {
        player_Summons_Base.isDead = true;
        player_Summons_Base.chaseRadius = 0;
        player_Summons_Base.attackRadius = 0;
        player_Summons_Base.cd.enabled = false;
        player_Summons_Base.enemyDetects.Clear();
        player_Summons_Base.chaseTargets.Clear();
    }
}