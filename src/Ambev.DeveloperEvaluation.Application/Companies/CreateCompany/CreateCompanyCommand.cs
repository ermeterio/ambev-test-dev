using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.CreateCompany
{
    public class CreateCompanyCommand : BaseCompany, IRequest<CreateCompanyResult>
    { }
}