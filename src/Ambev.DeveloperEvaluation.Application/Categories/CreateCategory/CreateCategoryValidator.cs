using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is mandatory");
            RuleFor(c => c.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is mandatory");
        }
    }
}