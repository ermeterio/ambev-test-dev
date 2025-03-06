using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, DeleteCompanyResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<DeleteCompanyHandler> _logger;

        public DeleteCompanyHandler(ICompanyRepository companyRepository, ILogger<DeleteCompanyHandler> logger)
        {
            _companyRepository = companyRepository;
            _logger = logger;
        }

        public async Task<DeleteCompanyResult> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCompanyValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var company = await _companyRepository.GetByIdAsync(request.Id);
            if (company == null)
                throw new KeyNotFoundException($"Company with ID {request.Id} not found");

            var success = await _companyRepository.DeleteAsync(company, cancellationToken);
            if (!success)
                throw new KeyNotFoundException($"Update for Id {request.Id} not successful");

            _logger.LogInformation("Company {@Company} deleted", company);

            return new DeleteCompanyResult { Success = true };
        }
    }
}