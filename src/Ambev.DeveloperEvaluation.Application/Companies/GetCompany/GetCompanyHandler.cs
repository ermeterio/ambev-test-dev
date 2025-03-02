using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany
{
    public class GetCompanyHandler : IRequestHandler<GetCompanyCommand, GetCompanyResult>
    {
        public Task<GetCompanyResult> Handle(GetCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}