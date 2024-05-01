public class Enemy_Summons_Hound_IdleState : Enemy_Summons_State
{
    Enemy_Summons_Hound enemy_Summons_Hound;
    public Enemy_Summons_Hound_IdleState(Enemy_Summons_Base summons_Hound_Base, Enemy_Summons_StateMachine stateMachine, string animBoolName, Enemy_Summons_Hound enemy_Summons_Hound) : base(summons_Hound_Base, stateMachine, animBoolName)
    {
        this.enemy_Summons_Hound = enemy_Summons_Hound;
    }

    public override void Eixt()
    {
        base.Eixt();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(enemy_Summons_Hound.houndMoveState);
        if (enemy_Summons_Hound.playerDetects.Count > 0)
            stateMachine.ChangeState(enemy_Summons_Hound.houndChaseState);
    }
}