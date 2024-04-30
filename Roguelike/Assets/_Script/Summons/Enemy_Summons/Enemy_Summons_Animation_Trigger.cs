using UnityEngine;

public class Enemy_Summons_Animation_Trigger : MonoBehaviour
{
    Enemy_Summons_Hound enemy_Summons_Hound => GetComponentInParent<Enemy_Summons_Hound>();

    private void AttackTrigger()
    {
        Debug.Log(enemy_Summons_Hound);
        if (enemy_Summons_Hound.cloestTarget != null)
        {
            enemy_Summons_Hound.cloestTarget.GetComponent<PlayerStats>()?.TakeDamage(enemy_Summons_Hound.damage);
            enemy_Summons_Hound.cloestTarget.GetComponent<PlayerBase>().isHit = true;
            enemy_Summons_Hound.cloestTarget.GetComponent<PlayerBase>().amountOfHit++;
        }
    }
}