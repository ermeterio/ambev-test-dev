﻿using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany
{
    public class GetCompanyHandler : IRequestHandler<GetCompanyCommand, GetCompanyResult>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        public GetCompanyHandler(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<GetCompanyResult> Handle(GetCompanyCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetCompanyValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var company = await _companyRepository.GetByIdAsync(request.Id);
            return company != null ? _mapper.Map<GetCompanyResult>(company) : new();
        }
    }
}