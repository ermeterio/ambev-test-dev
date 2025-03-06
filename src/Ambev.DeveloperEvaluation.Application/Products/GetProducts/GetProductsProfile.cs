using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsProfile : Profile
    {
        public GetProductsProfile()
        {
            CreateMap<GetProductsCommand, Product>();
            CreateMap<Product, BaseProduct>();
        }
    }
}