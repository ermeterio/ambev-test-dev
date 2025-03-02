using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany
{
    public class GetCompanyCommand : IRequest<GetCompanyResult>   
    {
        public Guid Id { get; set; }
    }
}