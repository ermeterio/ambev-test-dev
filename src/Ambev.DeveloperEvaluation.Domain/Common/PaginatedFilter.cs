namespace Ambev.DeveloperEvaluation.Domain.Common
{
    public class PaginatedFilter
    {
        public PaginatedFilter(int totalPages, int currentPage, int pageSize, int totalItems)
        {
            TotalPages = totalPages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}