using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private EnemyBase enemy => GetComponentInParent<EnemyBase>();
    private void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }
    private void SaberAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.stats.attackRadius.GetValue());
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerBase>() != null)
            {
                enemy.stats.meleeDoDamage(enemy.cloestPlayer.GetComponent<PlayerStats>());
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
