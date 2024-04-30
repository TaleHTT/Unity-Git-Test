﻿using UnityEngine;

public class Player_Summons_Animation_Trigger : MonoBehaviour
{
    Player_Summons_Hound player_Summons_Hound => GetComponentInParent<Player_Summons_Hound>();

    private void AttackTrigger()
    {
        Debug.Log(player_Summons_Hound);
        if (player_Summons_Hound.cloestTarget != null)
        {
            player_Summons_Hound.cloestTarget.GetComponent<EnemyStats>()?.TakeDamage(player_Summons_Hound.damage);
            player_Summons_Hound.cloestTarget.GetComponent<EnemyBase>().isHit = true;
            player_Summons_Hound.cloestTarget.GetComponent<EnemyBase>().amountOfHit++;
        }
    }
}