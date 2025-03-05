using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(c => c.CompanyId).NotNull().NotEmpty().WithMessage("Company Id is mandatory");
        RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is mandatory");
    }
}