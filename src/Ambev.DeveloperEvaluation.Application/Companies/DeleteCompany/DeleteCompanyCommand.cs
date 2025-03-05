using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany
{
    public class DeleteCompanyCommand : IRequest<DeleteCompanyResult>
    {
        public Guid Id { get; set; }
        public DeleteCompanyCommand(Guid id)
        {
            Id = id;
        }
    }
}