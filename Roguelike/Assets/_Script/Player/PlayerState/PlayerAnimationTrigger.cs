using UnityEngine;
public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    private Priest_Skill_Controller priest_Skill_Controller => GetComponent<Priest_Skill_Controller>();
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
    private void PriestTreatTrigger()
    {
        if (priest_Skill_Controller.treatTarget != null)
            priest_Skill_Controller.treatTarget.GetComponent<PlayerStats>().TakeTreat(priest_Skill_Controller.amountOfHealing);
    }
}
