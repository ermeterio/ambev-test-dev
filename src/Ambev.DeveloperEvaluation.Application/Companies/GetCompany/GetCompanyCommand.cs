using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany
{
    public class GetCompanyCommand : IRequest<GetCompanyResult>   
    {
        public GetCompanyCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}