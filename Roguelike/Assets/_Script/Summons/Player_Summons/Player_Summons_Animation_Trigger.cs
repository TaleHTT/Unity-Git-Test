using UnityEngine;

public class Player_Summons_Animation_Trigger : MonoBehaviour
{
    Player_Summons_Hound player_Summons_Hound_Controller => GetComponent<Player_Summons_Hound>();

    private void AttackTrigger()
    {
        if (player_Summons_Hound_Controller.cloestTarget != null)
            player_Summons_Hound_Controller.cloestTarget.GetComponent<EnemyStats>().TakeDamage(player_Summons_Hound_Controller.stats.baseDamage.GetValue());
    }
}