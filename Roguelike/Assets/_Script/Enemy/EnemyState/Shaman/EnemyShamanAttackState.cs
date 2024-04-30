public class EnemyShamanAttackState : EnemyShamanGroundState
{
    public EnemyShamanAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Shaman enemy_Shaman) : base(enemy, stateMachine, animboolName, enemy_Shaman)
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
            stateMachine.ChangeState(enemy_Shaman.shamanIdleState);
    }
}
