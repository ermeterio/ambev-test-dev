using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateUpdateBaseSaleProfile : Profile
{
    public CreateUpdateBaseSaleProfile()
    {
        CreateMap<CreateSaleRequest, CreateSaleCommand>();
        CreateMap<CreateSaleResult, CreateSaleResponse>();
        CreateMap<GetSaleResult, GetSaleResponse>();
        CreateMap<SaleItem, SaleItemResponse>();
        CreateMap<Product, ProductResponse>();
        CreateMap<SaleDiscount, SaleDiscountResponse>();
        CreateMap<Discount, DiscountResponse>();
    }
}