using Ambev.DeveloperEvaluation.WebApi.Features.Users.DeleteUser;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Base.Delete;

public class BaseDeleteRequestValidator : AbstractValidator<DeleteUserRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteRequest
    /// </summary>
    public BaseDeleteRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required");
    }
}