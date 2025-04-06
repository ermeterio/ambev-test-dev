namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts
{
    public class GetProductsResult
    {
        public IEnumerable<BaseProduct> Products { get; set; } = new List<BaseProduct>();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; }
    }
}