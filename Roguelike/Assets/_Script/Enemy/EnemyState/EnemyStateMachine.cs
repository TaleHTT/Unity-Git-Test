public class EnemyStateMachine
{
    public IEnemy currentState;
    public void Initialize(IEnemy state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void ChangeState(IEnemy state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
