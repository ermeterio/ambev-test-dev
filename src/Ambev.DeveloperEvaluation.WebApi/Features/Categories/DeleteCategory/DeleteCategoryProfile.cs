using Ambev.DeveloperEvaluation.Application.Categories.DeleteCategory;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Categories.DeleteCategory;

public class DeleteCategoryProfile : Profile
{
    public DeleteCategoryProfile()
    {
        CreateMap<Guid, DeleteCategoryCommand>()
            .ConstructUsing(id => new DeleteCategoryCommand(id));
    }
}