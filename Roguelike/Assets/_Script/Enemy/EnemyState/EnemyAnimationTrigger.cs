using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private EnemyBase enemy => GetComponentInParent<EnemyBase>();
    private void SaberAttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.transform.position, enemy.stats.attackRadius.GetValue());
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerStats>() != null)
            {
                if(hit.GetComponent<Sbaer_Skill_Controller>() != null)
                {
                    hit.GetComponent<Sbaer_Skill_Controller>().numOfHit++;
                }
                hit.GetComponent<PlayerStats>().TakeDamage(enemy.stats.damage.GetValue());
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
