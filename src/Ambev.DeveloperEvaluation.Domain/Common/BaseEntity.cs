using System.Security.Cryptography;
using Ambev.DeveloperEvaluation.Common.Validation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ambev.DeveloperEvaluation.Domain.Common;

public class BaseEntity : IComparable<BaseEntity>
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public Guid Id { get; set; } = Guid.NewGuid();
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

    protected static string GenerateShortGuid()
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Guid.NewGuid().ToByteArray());
        var base64 = Convert.ToBase64String(hashBytes);

        return new string(base64
                .Where(char.IsLetterOrDigit) 
                .Take(6) 
                .ToArray())
            .ToUpper();
    }

    public void AddDomainEvent(object domainEvent) => DomainEvents.Add(domainEvent);
    public void ClearDomainEvents() => DomainEvents.Clear();
}