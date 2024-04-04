using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private EnemyBase enemy => GetComponentInParent<EnemyBase>();
    private void SaberAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.stats.attackRadius.GetValue());
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                hit.GetComponent<PlayerBase>().stats.meleeTakeDamage(enemy.stats.damage.GetValue());
            }
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
