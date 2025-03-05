using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class CreateUpdateBaseSaleProfile : Profile
{
    public CreateUpdateBaseSaleProfile()
    {
        CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
        CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<SaleItem, SaleItemResponse>();
        CreateMap<Product, ProductResponse>();
        CreateMap<SaleDiscount, SaleDiscountResponse>();
        CreateMap<Discount, DiscountResponse>();
    }
}