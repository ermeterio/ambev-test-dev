namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public interface IEntityWithEvents
    {
        List<object> DomainEvents { get; }
        void AddDomainEvent(object domainEvent);
        void ClearDomainEvents();
    }
}