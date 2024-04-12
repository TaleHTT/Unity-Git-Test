using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private EnemyBase enemy => GetComponentInParent<EnemyBase>();
    private void SaberAttackTrigger()
    {
        if (enemy.cloestTarget != null)
        {
            enemy.cloestTarget.GetComponent<PlayerStats>().TakeDamage(enemy.stats.baseDamage.GetValue());
            enemy.cloestTarget.GetComponent<PlayerBase>().isHit = true;
        }
    }
    private void ArcherAttackTrigger()
    {
        enemy.AnimationArcherAttack();
    }
    private void CasterAttackTrigger()
    {
        enemy.AnimationCasterAttack();
    }
}
