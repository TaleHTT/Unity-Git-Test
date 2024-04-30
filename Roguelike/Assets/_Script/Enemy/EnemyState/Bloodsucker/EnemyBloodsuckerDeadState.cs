public class EnemyBloodsuckerDeadState : EnemyState
{
    public Enemy_Bloodsucker enemy_Bloodsucker;
    public EnemyBloodsuckerDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Bloodsucker enemy_Bloodsucker) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Bloodsucker = enemy_Bloodsucker;
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
        if (enemy_Bloodsucker.isDead == false)
            stateMachine.ChangeState(enemy_Bloodsucker.bloodsuckerIdleState);
    }
}
