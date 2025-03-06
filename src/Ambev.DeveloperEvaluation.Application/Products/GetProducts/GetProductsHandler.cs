using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsHandler : IRequestHandler<GetProductsCommand, GetProductsResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductsResult> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            var productFilter = _mapper.Map<Product>(request);
            var paginatedFilter = new PaginatedFilter(request.TotalPages, request.CurrentPage, request.PageSize,
                request.TotalItems);
            var products = await _productRepository.GetProductsByFilterAsync(productFilter, paginatedFilter, cancellationToken);
            var productsResponse = _mapper.Map<IEnumerable<BaseProduct>>(products.Item1);
            return new() { Products = productsResponse, CurrentPage = products.Item2.CurrentPage, TotalItems = products.Item2.TotalItems, TotalPages = products.Item2.TotalPages};
        }
    }
}