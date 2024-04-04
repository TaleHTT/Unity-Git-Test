using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPriestIdleState : PlayerState
{
    Player_Priest player_Priest;
    public PlayerPriestIdleState(PlayerBase player, PlayerStateMachine stateMachine, string animboolName, Player_Priest player_Priest) : base(player, stateMachine, animboolName)
    {
        this.player_Priest = player_Priest;
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
        for(int i = 0; i < player_Priest.playerDetects.Count; i++)
        {
            if (player_Priest.playerDetects[i].GetComponent<CharacterStats>().currentHealth < player_Priest.playerDetects[i].GetComponent<CharacterStats>().maxHp.GetValue())
                stateMachine.ChangeState(player_Priest.priestAttackState);
        }
    }
}
