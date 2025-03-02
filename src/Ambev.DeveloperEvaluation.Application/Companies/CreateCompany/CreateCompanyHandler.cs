using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.CreateCompany
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyResult>
    {
        public Task<CreateCompanyResult> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}