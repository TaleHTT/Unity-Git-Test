using System.Collections.Generic;
using UnityEngine;

public class PlayerSaberGroundState : PlayerState
{
    //public Vector3 target;
    public PlayerSaberGroundState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Saber player_Saber) : base(player, stateMachine, animboolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //player.playerAutoPathTarget = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //player.AutoPath();
        //if (player.pathPointList == null)
        //    return;
        //target = player.pathPointList[player.currentIndex];
        //if(player.transform.position != player.playerAutoPathTarget)
        //{
        //    player.transform.position = Vector3.MoveTowards(player.transform.position, target, player.stats.moveSpeed.GetValue() * Time.deltaTime);
            
        //}
        //else
        //{
        //    stateMachine.ChangeState(player_Saber.saberIdleState);
        //}
    }
}
