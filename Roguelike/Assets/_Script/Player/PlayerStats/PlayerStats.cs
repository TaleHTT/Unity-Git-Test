public class PlayerStats : CharacterStats
{
    private PlayerBase player;
    public override void Start()
    {
        base.Start();
        player = GetComponent<PlayerBase>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        player.DamageEffect();
    }
}
