namespace Ambev.DeveloperEvaluation.Domain.Events.Interface
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync();
        void AddEvent(object domainEvent);
    }
}