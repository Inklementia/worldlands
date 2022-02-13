namespace _Sources.Scripts.Infrastructure.GameStates
{
    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}