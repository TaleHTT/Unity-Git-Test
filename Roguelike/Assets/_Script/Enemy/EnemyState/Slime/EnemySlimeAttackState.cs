public class EnemySlimeAttackState : EnemySlimeGroundState
{
    public EnemySlimeAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Slime enemy_Slime) : base(enemy, stateMachine, animboolName, enemy_Slime)
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
            stateMachine.ChangeState(enemy_Slime.slimeIdleState);
    }
}
