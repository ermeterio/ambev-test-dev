using Ambev.DeveloperEvaluation.Application.Categories.GetCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.GetCategory;

public class GetCategoryProfile : Profile
{
    public GetCategoryProfile()
    {
        CreateMap<Guid, GetCategoryCommand>()
            .ConstructUsing(id => new GetCategoryCommand(id));
        CreateMap<GetCategoryResult, GetCategoryResponse>();
    }
}