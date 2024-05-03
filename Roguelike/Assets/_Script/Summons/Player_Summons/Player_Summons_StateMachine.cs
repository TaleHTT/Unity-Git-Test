public class Player_Summons_StateMachine
{
    public Player_Summons_State currentState;
    public void Initialize(Player_Summons_State state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void ChangeState(Player_Summons_State state)
    {
        currentState.Eixt();
        currentState = state;
        currentState.Enter();
    }
}