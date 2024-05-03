public class Enemy_Summons_Stats : EnemyStats
{
    public override void Start()
    {
        base.Start();
    }
    public override void TakeDamage(float damage, float percentage)
    {
        base.TakeDamage(damage, percentage);
    }
    public override void TakeTreat(float damage)
    {
        base.TakeTreat(damage);
    }
    public override void AuthenticTakeDamage(float damage)
    {
        base.AuthenticTakeDamage(damage);
    }
}