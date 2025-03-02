using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.UpdateCompany;

public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyRequestValidator()
    {
        RuleFor(company => company.Id).NotEmpty();
        RuleFor(company => company.Cnpj).NotEmpty().Length(14);
        RuleFor(company => company.Name).NotEmpty().Length(100);
    }
}