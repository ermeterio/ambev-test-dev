using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.GetCategory
{
    public class GetCategoryHandler : IRequestHandler<GetCategoryCommand, GetCategoryResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<GetCategoryResult> Handle(GetCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetCategoryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var category = await _categoryRepository.GetByIdAsync(request.Id);
            return category != null ? _mapper.Map<GetCategoryResult>(category) : new();
        }
    }
}