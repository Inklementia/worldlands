namespace _Sources.Scripts.Enemies.State_Mashine
{
    public class FiniteStateMashine
    {
        public State CurrentState { get; private set; }
        public State PreviousState { get; private set; }

        public void Initialize(State startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter(); // enter whatever state we start with (i. e. idle)
        }

        public void ChangeState(State newState)
        {
            PreviousState = CurrentState;
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();

      
        }
    }
}
