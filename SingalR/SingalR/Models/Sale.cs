using System.ComponentModel.DataAnnotations.Schema;

namespace SingalR.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer();

        [ForeignKey(nameof(ProductId))]
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        public DateTime PurchasedOn { get; set; }
    }
}
