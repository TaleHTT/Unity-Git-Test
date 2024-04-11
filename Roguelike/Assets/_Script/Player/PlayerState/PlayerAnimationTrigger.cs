using UnityEngine;
public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    private void SaberAttackTrigger()
    {
        if (player.closetEnemy != null)
            player.closetEnemy.GetComponent<EnemyStats>().TakeDamage(player.stats.damage.GetValue());
    }
    private void ArcherAttackTrigger()
    {
        player.AnimationArcherAttack();
    }
    private void CasterAttackTrigger()
    {
        player.AnimationCasterAttack();
    }
}
