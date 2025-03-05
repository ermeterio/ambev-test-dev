using Ambev.DeveloperEvaluation.Application.Products.CreateDiscount;
using Ambev.DeveloperEvaluation.Application.Products.DeleteDiscount;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateDiscount;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteDiscount;

public class DeleteDiscountProfile : Profile
{
    public DeleteDiscountProfile()
    {
        CreateMap<Guid, DeleteDiscountCommand>()
            .ConstructUsing(id => new DeleteDiscountCommand(id));
        CreateMap<CreateDiscountResult, CreateDiscountResponse>();
    }
}