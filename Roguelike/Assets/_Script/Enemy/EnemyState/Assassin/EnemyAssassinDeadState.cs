public class EnemyAssassinDeadState : EnemyState
{
    public Enemy_Assassin enemy_Assassin;
    public EnemyAssassinDeadState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Assassin enemy_Assassin) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Assassin = enemy_Assassin;
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
        if (enemy_Assassin.isDead == false)
            stateMachine.ChangeState(enemy_Assassin.assassinIdleState);
    }
}
