﻿using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAssassinGroundState : PlayerState
{
    Player_Assassin player_Assassin;
    private List<Vector3> pathPointList;
    private int currentIndex;
    private Vector3 CenterPointAutoPathTarget;
    private Vector3 target;
    public PlayerAssassinGroundState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName) : base(player, stateMachine, animboolName)
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
        if(player_Assassin.isStealth == true)
        {
            player_Assassin.durationTimer -= Time.deltaTime;
            if (player_Assassin.durationTimer <= 0)
            {
                player_Assassin.isStealth = false;
                stateMachine.ChangeState(player_Assassin.assassinIdleState);
            }
        }
        AutoPath();
        if (pathPointList == null)
            return;
        target = pathPointList[currentIndex];
        if (player_Assassin.transform.position != CenterPointAutoPathTarget)
        {
            if (Input.GetMouseButton(0))
            {
                player_Assassin.transform.position = Vector3.MoveTowards(player_Assassin.transform.position, target, player_Assassin.stats.moveSpeed.GetValue() * Time.deltaTime);
            }
        }
    }
    public void GeneratePath(Vector3 target)
    {
        currentIndex = 0;
        player_Assassin.seeker.StartPath(player_Assassin.transform.position, target, Path =>
        {
            pathPointList = Path.vectorPath;
        });
    }
    public void AutoPath()
    {
        CenterPointAutoPathTarget = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        GeneratePath(CenterPointAutoPathTarget);
        if (pathPointList == null || pathPointList.Count == 0)
        {
            GeneratePath(CenterPointAutoPathTarget);
        }
        else if (Vector2.Distance(player_Assassin.transform.position, pathPointList[currentIndex]) <= .1f)
        {
            currentIndex++;
            if (currentIndex >= pathPointList.Count)
                GeneratePath(CenterPointAutoPathTarget);
        }
    }
}