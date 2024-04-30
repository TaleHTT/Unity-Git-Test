public class EnemySlimeDeadState : EnemyState
{
    public Enemy_Slime enemy_Slime;
    public EnemySlimeDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Slime enemy_Slime) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Slime = enemy_Slime;
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
        if (enemy_Slime.isDead == false)
            stateMachine.ChangeState(enemy_Slime.slimeIdleState);
    }
}
