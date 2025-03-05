using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory
{
    public class GetCategoryProfile : Profile
    {
        public GetCategoryProfile()
        {
            CreateMap<Category, GetCategoryResult>();
        }
    }
}