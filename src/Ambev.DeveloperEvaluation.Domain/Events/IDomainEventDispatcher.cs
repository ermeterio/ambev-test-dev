namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface IDomainEventDispatcher
    {
        Task DispatchEventsAsync();
        void AddEvent(object domainEvent);
    }
}