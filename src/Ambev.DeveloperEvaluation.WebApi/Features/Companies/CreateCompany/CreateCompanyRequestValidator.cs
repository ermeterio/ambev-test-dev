using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.CreateCompany;

public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
    {
        RuleFor(company => company.Cnpj).NotEmpty().Length(14);
        RuleFor(company => company.Name).NotEmpty().Length(100);
    }
}