using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany
{
    public class UpdateCompanyValidator : BaseCompanyValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyValidator()
        {
            RuleFor(company => company.Id)
                .NotNull().NotEmpty().WithMessage("Id is mandatory");
        }
    }
}