public class Player_Summons_Stats : CharacterStats
{
    private Player_Summons_Hound player_Summons_Hound_Controllerp;
    public override void Start()
    {
        base.Start();
        player_Summons_Hound_Controllerp = GetComponent<Player_Summons_Hound>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        player_Summons_Hound_Controllerp.DamageEffect();
    }
}