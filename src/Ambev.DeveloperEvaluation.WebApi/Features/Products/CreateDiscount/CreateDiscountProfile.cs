using Ambev.DeveloperEvaluation.Application.Products.CreateDiscount;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateDiscount;

public class CreateDiscountProfile : Profile
{
    public CreateDiscountProfile()
    {
        CreateMap<CreateDiscountRequest, CreateDiscountCommand>();
        CreateMap<CreateDiscountResult, CreateDiscountResponse>();
    }
}