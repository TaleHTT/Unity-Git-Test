using UnityEngine;
public class PlayerAnimationTrigger : MonoBehaviour
{
    private PlayerBase player => GetComponentInParent<PlayerBase>();
    private void SaberAttackTrigger()
    {
        if(player.closetEnemy != null)
            player.closetEnemy.GetComponent<CharacterStats>().meleeTakeDamage(player.stats.damage.GetValue());
    }
    private void ArcherAttackTrigger()
    {
        player.AnimationArcherAttack();
    }
    private void CasterAttackTrigger()
    {
        player.AnimationCasterAttack();
    }
    private void PriestAttackTrigger()
    {
        if(player.treatTarget != null)
            player.treatTarget.GetComponent<CharacterStats>().treatTakeDamage(player.stats.damage.GetValue());
    }
}
