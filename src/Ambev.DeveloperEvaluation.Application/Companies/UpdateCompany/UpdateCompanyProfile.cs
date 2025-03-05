using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany
{
    public class UpdateCompanyProfile : Profile
    {
        public UpdateCompanyProfile()
        {
            CreateMap<UpdateCompanyCommand, Company>();
            CreateMap<Company, UpdateCompanyResult>();
        }
    }
}
