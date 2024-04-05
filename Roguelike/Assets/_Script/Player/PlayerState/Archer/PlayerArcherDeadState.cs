using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherDeadState : PlayerState
{
    public PlayerArcherDeadState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Archer player_Archer) : base(player, stateMachine, animboolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        DeadLogci();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
