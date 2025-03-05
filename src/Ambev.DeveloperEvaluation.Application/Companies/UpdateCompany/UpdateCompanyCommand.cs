using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany
{
    public class UpdateCompanyCommand : BaseCompany, IRequest<UpdateCompanyResult>
    {
        public Guid Id { get; set; }
    }
}