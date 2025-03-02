using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Companies.CreateCompany
{
    public class CreateCompanyProfile : Profile
    {
        public CreateCompanyProfile()
        {
            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<Company, CreateCompanyResult>();
        }
    }
}