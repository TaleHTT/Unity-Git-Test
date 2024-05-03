public class EnemyIceCasterDeadState : EnemyState
{
    public Enemy_IceCaster enemy_IceCaster;
    public EnemyIceCasterDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_IceCaster enemy_IceCaster) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_IceCaster = enemy_IceCaster;
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
        if (enemy_IceCaster.isDead == false)
            stateMachine.ChangeState(enemy_IceCaster.iceCasterIdleState);
    }
}
