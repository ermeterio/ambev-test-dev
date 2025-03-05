using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Categories.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(ICategoryRepository categoryRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCategoryValidator();
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            _ = _companyRepository.GetByIdAsync(request.CompanyId) ??
                          throw new InvalidOperationException("Company not found");

            var categoryExists = await _categoryRepository.GetByNameAsync(request.Name, request.CompanyId);
            if(categoryExists is not null)
            {
                throw new InvalidOperationException("Category already exists");
            }

            var category = _mapper.Map<Category>(request);

            var categoryInsert = await _categoryRepository.AddAsync(category, cancellationToken) ??
                                 throw new InvalidOperationException("Category not created");

            return _mapper.Map<CreateCategoryResult>(categoryInsert);
        }
    }
}