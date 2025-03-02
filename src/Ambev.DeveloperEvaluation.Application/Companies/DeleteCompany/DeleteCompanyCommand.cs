using Ambev.DeveloperEvaluation.Application.Base.Delete;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany
{
    public class DeleteCompanyCommand : BaseDeleteCommand, IRequest<DeleteCompanyResult>
    { }
}