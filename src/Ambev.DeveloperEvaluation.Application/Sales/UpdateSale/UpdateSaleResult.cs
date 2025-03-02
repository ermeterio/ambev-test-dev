namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult : BaseSale
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
    }
}