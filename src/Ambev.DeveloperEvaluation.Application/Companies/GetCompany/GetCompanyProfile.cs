using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany
{
    public class GetCompanyProfile : Profile
    {
        public GetCompanyProfile()
        {
            CreateMap<Company, GetCompanyResult>();
        }
    }
}