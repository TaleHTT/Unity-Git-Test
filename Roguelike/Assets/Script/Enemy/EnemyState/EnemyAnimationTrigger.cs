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
                    AttackLogic();
                }
            }
        }
    }
    private void ArcherAttackTrigger()
    {
        enemy.AnimationArcherAttack();
    }
    private void AttackLogic()
    {
        if (enemy.playerDetects.Count >= 3)
        {
            for (int i = 1; i < enemy.playerDetects.Count - 1; i++)
            {
                enemy.stats.DoDamage((Vector2.Distance(enemy.transform.position, enemy.playerDetects[i].transform.position) > Vector2.Distance(enemy.transform.position, enemy.playerDetects[i + 1].transform.position)) ? ((Vector2.Distance(enemy.transform.position, enemy.playerDetects[i].transform.position) > Vector2.Distance(enemy.transform.position, enemy.playerDetects[i - 1].transform.position)) ? enemy.playerDetects[i].GetComponent<EnemyStats>() : enemy.playerDetects[i - 1].GetComponent<EnemyStats>()) : ((Vector2.Distance(enemy.transform.position, enemy.playerDetects[i + 1].transform.position) > Vector2.Distance(enemy.transform.position, enemy.playerDetects[i - 1].transform.position)) ? enemy.playerDetects[i + 1].GetComponent<EnemyStats>() : enemy.playerDetects[i - 1].GetComponent<EnemyStats>()));
            }
        }
        else if (enemy.playerDetects.Count == 2)
        {
            for (int i = 0; i < enemy.playerDetects.Count - 1; i++)
            {
                if (Vector2.Distance(enemy.transform.position, enemy.playerDetects[i].transform.position) >
                    Vector2.Distance(enemy.transform.position, enemy.playerDetects[i + 1].transform.position))
                {
                    enemy.playerDetects[i].GetComponent<EnemyStats>();
                    enemy.stats.DoDamage(enemy.playerDetects[i].GetComponent<EnemyStats>());

                }
                else
                {
                    enemy.playerDetects[i + 1].GetComponent<EnemyStats>();
                    enemy.stats.DoDamage(enemy.playerDetects[i].GetComponent<EnemyStats>());
                }
            }
        }
    }
}
