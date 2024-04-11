public class EnemySaberGroundState : EnemyState
{
    public Enemy_Saber enemy_Saber;
    public EnemySaberGroundState(EnemyBase enemy, EnemyStateMachine stateMachine, string animboolName, Enemy_Saber enemy_Saber) : base(enemy, stateMachine, animboolName)
    {
        this.enemy_Saber = enemy_Saber;
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
        if (enemy_Saber.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Saber.saberChaseState);
        if (enemy_Saber.attackDetects.Count > 0)
            stateMachine.ChangeState(enemy_Saber.saberAttackState);
    }
}