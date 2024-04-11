public class EnemySaberAttackState : EnemySaberGroundState
{
    public EnemySaberAttackState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Saber enemy_Saber) : base(enemy, stateMachine, animboolName, enemy_Saber)
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
        if (enemy_Saber.attackDetects.Count <= 0)
            stateMachine.ChangeState(enemy_Saber.saberIdleState);
    }
}
