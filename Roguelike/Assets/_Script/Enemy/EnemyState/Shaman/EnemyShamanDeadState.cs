public class EnemyShamanDeadState : EnemyState
{
    public Enemy_Shaman enemy_Shaman;
    public EnemyShamanDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Shaman enemy_Shaman) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Shaman = enemy_Shaman;
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
        if (enemy_Shaman.isDead == false)
            stateMachine.ChangeState(enemy_Shaman.shamanIdleState);
    }
}
