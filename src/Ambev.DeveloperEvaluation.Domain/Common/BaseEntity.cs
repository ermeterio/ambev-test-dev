using Ambev.DeveloperEvaluation.Common.Validation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    [BsonIgnore]
    public Guid Id { get; set; }

    [NotMapped]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId BsonId { get; set; } = new ();

    [BsonIgnore]
    public Guid CompanyId { get; set; }
    public bool IsDeleted { get; set; }
    public List<object> DomainEvents { get; } = new();

    public Task<IEnumerable<ValidationErrorDetail>> ValidateAsync()
    {
        return Validator.ValidateAsync(this);
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other!.Id.CompareTo(Id);
    }

    public void AddDomainEvent(object domainEvent) => DomainEvents.Add(domainEvent);
    public void ClearDomainEvents() => DomainEvents.Clear();
}