public class StateMachine
{
    public IState CurrentState { get; private set; }

    public void ChangeState(IState newState)
    {
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

    public void Tick()
    {
        CurrentState?.TickState();
    }
}

