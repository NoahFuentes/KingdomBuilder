public class StateMachine
{
    public BaseState CurrentState { get; private set; }

    public void ChangeState(BaseState newState)
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

