public class EnemyTwoHandedSaberAttackState : EnemyTwoHandedSaberGroundState
{
    public EnemyTwoHandedSaberAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_TwoHandedSaber enemy_TwoHandedSaber) : base(enemy, stateMachine, animboolName, enemy_TwoHandedSaber)
    {
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
        enemy.anim.speed = enemy.stats.attackSpeed.GetValue() + defaultAttackSpeed;
        if (triggerCalled)
            stateMachine.ChangeState(enemy_TwoHandedSaber.twoHandedSaberIdleState);
    }
}
