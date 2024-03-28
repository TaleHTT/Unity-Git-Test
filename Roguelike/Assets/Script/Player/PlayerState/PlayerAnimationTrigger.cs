using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void SaberAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.stats.attackRadius.GetValue());
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                player.stats.meleeDoDamage(player.closetEnemy.GetComponent<EnemyStats>());
            }
        }
    }
    private void ArcherAttackTrigger()
    {
        player.AnimationArcherAttack();
    }
    private void CasterAttackTrigger()
    {
        player.AnimationCasterAttack();
    }
    private void PriestAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.stats.attackRadius.GetValue());
        foreach (var hit in colliders)
        {
            if(hit.GetComponent<PlayerBase>() != null)
            {
                player.stats.treatDoDamage(hit.GetComponent<PlayerStats>());
            }
        }
    }
}
