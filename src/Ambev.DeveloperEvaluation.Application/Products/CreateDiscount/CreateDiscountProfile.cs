using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateDiscount
{
    public class CreateDiscountProfile : Profile
    {
        public CreateDiscountProfile()
        {
            CreateMap<CreateDiscountCommand, Discount>();
            CreateMap<Discount, CreateDiscountResult>();
        }
    }
}