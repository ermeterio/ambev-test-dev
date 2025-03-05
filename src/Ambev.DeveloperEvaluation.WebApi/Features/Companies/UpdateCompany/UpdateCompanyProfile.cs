using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.UpdateCompany;

public class UpdateCompanyProfile : Profile
{
    public UpdateCompanyProfile()
    {
        CreateMap<UpdateCompanyRequest, UpdateCompanyCommand>();
        CreateMap<UpdateCompanyResult, UpdateCompanyResponse>();
    }
}