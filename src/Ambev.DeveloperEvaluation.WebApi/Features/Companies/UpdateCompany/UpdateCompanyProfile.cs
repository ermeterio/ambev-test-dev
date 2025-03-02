using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.WebApi.Features.Companies.CreateCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.UpdateCompany;

public class UpdateCompanyProfile : Profile
{
    public UpdateCompanyProfile()
    {
        CreateMap<CreateCompanyRequest, UpdateCompanyCommand>();
    }
}