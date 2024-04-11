public class Player_Summons_Hound_DeadState : Player_Summons_State
{
    public Player_Summons_Hound_DeadState(Player_Summons_Hound summons_Hound_Controller, Player_Summons_StateMachine stateMachine, string animBoolName) : base(summons_Hound_Controller, stateMachine, animBoolName)
    {
    }

    public override void Eixt()
    {
        base.Eixt();
    }

    public override void Enter()
    {
        base.Enter();
        DeadLogic();
    }

    public override void Update()
    {
        base.Update();
    }
}