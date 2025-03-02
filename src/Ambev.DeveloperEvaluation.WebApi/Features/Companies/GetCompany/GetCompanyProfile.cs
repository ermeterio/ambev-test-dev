using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Companies.GetCompany;

public class GetCompanyProfile : Profile
{
    public GetCompanyProfile()
    {
        CreateMap<Guid, Application.Companies.GetCompany.GetCompanyCommand>()
            .ConstructUsing(id => new Application.Companies.GetCompany.GetCompanyCommand(id));
    }
}