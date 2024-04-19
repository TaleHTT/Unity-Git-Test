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

    public Player_Summons_Hound player_Summons_Hound;
    public Player_Summons_StateMachine stateMachine;
    private string animBoolName;
    public Player_Summons_State(Player_Summons_Hound summons_Hound_Controller, Player_Summons_StateMachine stateMachine, string animBoolName)
    {
        this.player_Summons_Hound = summons_Hound_Controller;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Eixt()
    {
        player_Summons_Hound.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {

    }

    public virtual void Enter()
    {
        player_Summons_Hound.anim.SetBool(animBoolName, true);
    }
    public void AutoPath()
    {
        pathGenerateTimer += Time.deltaTime;
        if (pathGenerateTimer >= pathGenerateInterval)
        {
            GeneratePath(player_Summons_Hound.cloestTarget.transform.position);
            pathGenerateTimer = 0;
        }
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(player_Summons_Hound.cloestTarget.transform.position);
        }
        else if (Vector2.Distance(player_Summons_Hound.transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(player_Summons_Hound.cloestTarget.transform.position);
        }
    }
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        player_Summons_Hound.seeker.StartPath(player_Summons_Hound.transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
    public void DeadLogic()
    {
        player_Summons_Hound.isDead = true;
        player_Summons_Hound.chaseRadius = 0;
        player_Summons_Hound.attackRadius = 0;
        player_Summons_Hound.cd.enabled = false;
        player_Summons_Hound.attackDetects.Clear();
        player_Summons_Hound.attackDetects.Clear();
    }
}