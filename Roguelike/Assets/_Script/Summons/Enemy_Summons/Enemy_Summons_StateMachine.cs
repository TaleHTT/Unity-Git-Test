public class Enemy_Summons_StateMachine
{
    public Enemy_Summons_State currentState;
    public void Initialize(Enemy_Summons_State state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void ChangeState(Enemy_Summons_State state)
    {
        currentState.Eixt();
        currentState = state;
        currentState.Enter();
    }
}