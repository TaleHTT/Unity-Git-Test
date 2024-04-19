public class Player_Summons_Hound_AttackState : Player_Summons_State
{
    public Player_Summons_Hound_AttackState(Player_Summons_Hound summons_Hound_Controller, Player_Summons_StateMachine stateMachine, string animBoolName) : base(summons_Hound_Controller, stateMachine, animBoolName)
    {
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
        if (player_Summons_Hound.attackDetects.Count <= 0)
            stateMachine.ChangeState(player_Summons_Hound.houndIdleState);
    }
}