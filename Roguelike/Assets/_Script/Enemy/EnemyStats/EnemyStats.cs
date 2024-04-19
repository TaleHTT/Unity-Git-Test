public class EnemyStats : CharacterStats
{
    EnemyBase enemy;
    public override void Start()
    {
        base.Start();
        enemy = GetComponent<EnemyBase>();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        enemy.DamageEffect();
    }
    public override void TakeTreat(float damage)
    {
        base.TakeTreat(damage);
    }
    public override void AuthenticTakeDamage(float damage)
    {
        base.AuthenticTakeDamage(damage);
        enemy.DamageEffect();
    }
}
