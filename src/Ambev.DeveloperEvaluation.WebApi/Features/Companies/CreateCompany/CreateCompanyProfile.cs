using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.CreateCompany;

public class CreateCompanyProfile : Profile
{
    public CreateCompanyProfile()
    {
        CreateMap<CreateCompanyRequest, CreateCompanyCommand>();
        CreateMap<CreateCompanyResult, CreateCompanyResponse>();
    }
}