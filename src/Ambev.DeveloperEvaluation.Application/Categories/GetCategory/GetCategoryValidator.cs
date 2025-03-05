using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory
{
    public class GetCategoryValidator : AbstractValidator<GetCategoryCommand>
    {
        public GetCategoryValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
        }
    }
}
