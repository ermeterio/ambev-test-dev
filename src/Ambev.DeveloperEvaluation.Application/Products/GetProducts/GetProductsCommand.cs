using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsCommand : IRequest<GetProductsResult>
    {
        public string Name { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid CompanyId { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 30;
        public int TotalItems { get; set; }
    }
}