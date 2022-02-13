namespace _Sources.Scripts.Infrastructure.GameStates
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}