using UnityEngine;

public class Player_Summons_Animation_Trigger : MonoBehaviour
{
    Player_Summons_Hound player_Summons_Hound => GetComponentInParent<Player_Summons_Hound>();

    private void AttackTrigger()
    {
        Debug.Log(player_Summons_Hound);
        if (player_Summons_Hound.closetEnemy != null)
        {
            player_Summons_Hound.closetEnemy.GetComponent<EnemyStats>()?.TakeDamage(player_Summons_Hound.stats.damage.GetValue());
            player_Summons_Hound.closetEnemy.GetComponent<EnemyBase>().isHit = true;
            player_Summons_Hound.closetEnemy.GetComponent<EnemyBase>().amountOfHit++;
        }
    }
}