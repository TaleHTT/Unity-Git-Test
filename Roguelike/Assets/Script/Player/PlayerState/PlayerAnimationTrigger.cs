using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerAnimationTrigger : MonoBehaviour
{
    PlayerBase player => GetComponentInParent<PlayerBase>();
    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, player.stats.attackRadius.GetValue());
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<EnemyBase>() != null)
            {
                if (player.enemyDetects.Count == 1)
                {
                    EnemyStats target = hit.GetComponent<EnemyStats>();
                    player.stats.DoDamage(target);
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
        player.AnimationArcherAttack();
    }
    public void AttackLogic()
    {
        if (player.enemyDetects.Count >= 3)
        {
            for (int i = 1; i < player.enemyDetects.Count - 1; i++)
            {
                player.stats.DoDamage((Vector2.Distance(player.transform.position, player.enemyDetects[i].transform.position) > Vector2.Distance(player.transform.position, player.enemyDetects[i + 1].transform.position)) ? ((Vector2.Distance(player.transform.position, player.enemyDetects[i].transform.position) > Vector2.Distance(player.transform.position, player.enemyDetects[i - 1].transform.position)) ? player.enemyDetects[i].GetComponent<EnemyStats>() : player.enemyDetects[i - 1].GetComponent<EnemyStats>()) : ((Vector2.Distance(player.transform.position, player.enemyDetects[i + 1].transform.position) > Vector2.Distance(player.transform.position, player.enemyDetects[i - 1].transform.position)) ? player.enemyDetects[i + 1].GetComponent<EnemyStats>() : player.enemyDetects[i - 1].GetComponent<EnemyStats>()));
            }
        }
        else if(player.enemyDetects.Count == 2)
        {
            for (int i = 0; i < player.enemyDetects.Count - 1; i++)
            {
                if (Vector2.Distance(player.transform.position, player.enemyDetects[i].transform.position) >
                    Vector2.Distance(player.transform.position, player.enemyDetects[i + 1].transform.position))
                {
                    player.enemyDetects[i].GetComponent<EnemyStats>();
                    player.stats.DoDamage(player.enemyDetects[i].GetComponent<EnemyStats>());

                }
                else
                {
                    player.enemyDetects[i + 1].GetComponent<EnemyStats>();
                    player.stats.DoDamage(player.enemyDetects[i].GetComponent<EnemyStats>());
                }
            }
        }
    }
}
