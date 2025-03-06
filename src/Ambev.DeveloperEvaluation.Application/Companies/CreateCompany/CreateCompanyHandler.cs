using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Companies.CreateCompany
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ILogger<CreateCompanyHandler> _logger;
        private readonly IMapper _mapper;

        public CreateCompanyHandler(ICompanyRepository companyRepository, IMapper mapper, ILogger<CreateCompanyHandler> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateCompanyResult> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCompanyValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingCompany = await _companyRepository.GetByCnpjAsync(request.Cnpj, cancellationToken);
            if(existingCompany is not null)
                throw new InvalidOperationException($"Company with CNPJ {request.Cnpj} already exists");

            _logger.LogInformation("Creating company {@Company}", request);

            var company = _mapper.Map<Company>(request);
            var createdCompany = await _companyRepository.AddAsync(company, cancellationToken);
            if(createdCompany is null)
                throw new InvalidOperationException("Error creating company");

            return _mapper.Map<CreateCompanyResult>(createdCompany);
        }
    }
}