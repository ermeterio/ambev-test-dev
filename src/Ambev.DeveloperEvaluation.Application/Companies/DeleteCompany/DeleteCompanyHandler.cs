using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, DeleteCompanyResult>
    {
        public Task<DeleteCompanyResult> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}