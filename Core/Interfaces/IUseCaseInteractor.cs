namespace Notifier.Core.Interfaces
{
    public interface IUseCaseInteractor<TRequest, TResponse>
    {
        TResponse Handle(TRequest useCaseRequest);
    }   
}