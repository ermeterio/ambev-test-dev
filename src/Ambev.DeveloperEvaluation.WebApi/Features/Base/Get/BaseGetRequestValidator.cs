using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Base.Get;

public class BaseGetRequestValidator : AbstractValidator<BaseGetRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteRequest
    /// </summary>
    public BaseGetRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}