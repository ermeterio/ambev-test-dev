using Ambev.DeveloperEvaluation.Application.Categories.CreateCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.CreateCategory;

public class CreateCategoryProfile : Profile
{
    public CreateCategoryProfile()
    {
        CreateMap<CreateCategoryRequest, CreateCategoryCommand>();
        CreateMap<CreateCategoryResult, CreateCategoryResponse>();
    }
}