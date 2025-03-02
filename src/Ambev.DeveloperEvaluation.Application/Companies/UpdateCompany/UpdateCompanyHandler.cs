using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, UpdateCompanyResult>
    {
        public Task<UpdateCompanyResult> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}