public class EnemyTwoHandedSaberGroundState : EnemyState
{
    public Enemy_TwoHandedSaber enemy_TwoHandedSaber;
    public EnemyTwoHandedSaberGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_TwoHandedSaber enemy_TwoHandedSaber) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_TwoHandedSaber = enemy_TwoHandedSaber;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy_TwoHandedSaber.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_TwoHandedSaber.twoHandedSaberChaseState);
        if (enemy_TwoHandedSaber.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_TwoHandedSaber.twoHandedSaberAttackState);
        if (enemy_TwoHandedSaber.stats.currentHealth <= 0)
            stateMachine.ChangeState(enemy_TwoHandedSaber.twoHandedSaberDeadState);
    }
}