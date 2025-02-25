using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.CreateUpdateSale;

public class CreateUpdateBaseSaleProfile : Profile
{
    public CreateUpdateBaseSaleProfile()
    {
        CreateMap<CreateUpdateSaleRequest, CreateUpdateBaseSaleCommand>()
            .ConstructUsing(request => new CreateUpdateBaseSaleCommand(request.Id, request.SaleDate, request.CompanyId, request.SaleValue));
    }
}