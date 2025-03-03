using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany
{
    public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, UpdateCompanyResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public UpdateCompanyHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<UpdateCompanyResult> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCompanyValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var company = await _companyRepository.GetByIdAsync(request.Id);
            if (company is null)
                throw new InvalidOperationException($"Company with Id {request.Id} not found");

            var companyMapper = _mapper.Map<Company>(request);
            company.Update(companyMapper);

            var updatedCompany = await _companyRepository.UpdateAsync(company, cancellationToken);

            return updatedCompany is null ? new() { Success = false } : _mapper.Map<UpdateCompanyResult>(updatedCompany);
        }
    }
}