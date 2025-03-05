using Ambev.DeveloperEvaluation.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.Domain.Entities.Product
{
    public class Rating : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid UserId { get; set; }
        public User.User? User { get; set; }
        [Range(1, 5)]
        public int Rate { get; set; }
        public required string Comment { get; set; }
    }
}