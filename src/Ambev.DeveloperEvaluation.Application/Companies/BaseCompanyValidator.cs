using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Companies
{
    public class BaseCompanyValidator<T> : AbstractValidator<T> where T : BaseCompany
    {
        public BaseCompanyValidator()
        {
            RuleFor(c => c.Cnpj).NotEmpty().Length(14);
            RuleFor(c => c.Name).NotEmpty().MaximumLength(100);
        }
    }
}