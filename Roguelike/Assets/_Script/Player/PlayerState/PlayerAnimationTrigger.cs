using System.Threading;
using UnityEngine;
public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    private Saber_Skill_Controller saber_Skill_Controller => GetComponent<Saber_Skill_Controller>();
    private Priest_Skill_Controller priest_Skill_Controller => GetComponent<Priest_Skill_Controller>();
    private void SaberAttackTrigger()
    {
        if (player.closetEnemy != null && saber_Skill_Controller.isHave_X_Equipment == false)
            player.closetEnemy.GetComponent<EnemyStats>()?.TakeDamage(player.stats.baseDamage.GetValue());

        else if(player.closetEnemy != null && saber_Skill_Controller.isHave_X_Equipment == true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, player.attackRadius, player.whatIsEnemy);
            foreach(var hit in colliders)
            {
                if(hit.GetComponent<EnemyStats>() != null)
                    hit.GetComponent<EnemyStats>()?.TakeDamage(player.stats.actualDamage);
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
    private void PriestTreatTrigger()
    {
        if (priest_Skill_Controller.treatTarget != null)
        {
            priest_Skill_Controller.treatTarget.GetComponent<PlayerStats>()?.TakeTreat(priest_Skill_Controller.amountOfHealing);
            priest_Skill_Controller.numberOfTreatments++;
        }
    }
}
