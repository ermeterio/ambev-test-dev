using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateUpdateBaseSaleProfile : Profile
{
    public CreateUpdateBaseSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
    }
}