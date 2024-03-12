using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    EnemyBase enemy => GetComponentInParent<EnemyBase>();
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
                if (enemy.playerDetects.Count == 1)
                {
                    PlayerStats target = hit.GetComponent<PlayerStats>();
                    enemy.stats.DoDamage(target);
                }
                else
                {
                    enemy.AttackLogic();
                }
            }
        }
    }
    private void ArcherAttackTrigger()
    {
        enemy.AnimationArcherAttack();
    }
}
