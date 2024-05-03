public class EnemyPriestDeadState : EnemyState
{
    Enemy_Priest enemy_Priest;
    public EnemyPriestDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Priest enemy_Priest) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Priest = enemy_Priest;
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
        if (enemy_Priest.isDead == false)
            stateMachine.ChangeState(enemy_Priest.priestDeadState);
    }
}
