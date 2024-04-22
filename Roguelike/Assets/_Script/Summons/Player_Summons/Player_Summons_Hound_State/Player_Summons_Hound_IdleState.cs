public class Player_Summons_Hound_IdleState : Player_Summons_State
{
    Player_Summons_Hound player_Summons_Hound;
    public Player_Summons_Hound_IdleState(Player_Summons_Hound summons_Hound_Controller, Player_Summons_StateMachine stateMachine, string animBoolName, Player_Summons_Hound player_Summons_Hound) : base(summons_Hound_Controller, stateMachine, animBoolName)
    {
        this.player_Summons_Hound = player_Summons_Hound;
    }

    public override void Eixt()
    {
        base.Eixt();
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            stateMachine.ChangeState(player_Summons_Hound.houndMoveState);
        if (player_Summons_Hound.enemyDetects.Count > 0)
            stateMachine.ChangeState(player_Summons_Hound.houndChaseState);
    }
}