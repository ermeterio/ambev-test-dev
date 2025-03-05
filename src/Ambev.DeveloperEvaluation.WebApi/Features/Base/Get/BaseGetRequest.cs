namespace Ambev.DeveloperEvaluation.WebApi.Features.Base.Get;

public class BaseGetRequest
{
    /// <summary>
    /// The unique identifier of the entity to retrieve
    /// </summary>
    public Guid Id { get; set; }
}