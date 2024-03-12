using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherGroundState : PlayerState
{
    public Vector3 target;
    public Player_Archer player_Archer;
    public PlayerArcherGroundState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Archer player_Archer) : base(player, stateMachine, animboolName)
    {
        this.player_Archer = player_Archer;
    }

    public override void Enter()
    {
        base.Enter();
        target = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        player.AutoPath();
        if (player.pathPointList == null)
            return;
        target = player.pathPointList[player.currentIndex];
        if (player.transform.position != player.playerAutoPathTarget)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, player.stats.moveSpeed.GetValue() * Time.deltaTime);

        }
        else
        {
            stateMachine.ChangeState(player_Archer.archerIdleState);
        }
    }
}
