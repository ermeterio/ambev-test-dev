using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty().WithMessage("Id is mandatory");
        }
    }
}
