namespace Ambev.DeveloperEvaluation.Domain.Events.Interface
{
    public interface IEntityWithEvents
    {
        List<object> DomainEvents { get; }
        void AddDomainEvent(object domainEvent);
        void ClearDomainEvents();
    }
}