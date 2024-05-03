public class EnemyTwoHandedSaberDeadState : EnemyState
{
    public Enemy_TwoHandedSaber enemy_TwoHandedSaber;
    public EnemyTwoHandedSaberDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_TwoHandedSaber enemy_TwoHandedSaber) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_TwoHandedSaber = enemy_TwoHandedSaber;
    }

    public override void Enter()
    {
        base.Enter();
        DeadLogci();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy_TwoHandedSaber.isDead == false)
            stateMachine.ChangeState(enemy_TwoHandedSaber.twoHandedSaberIdleState);
    }
}
