using System.ComponentModel.DataAnnotations.Schema;

namespace SingalR.Models
{
    public class Sale
    {
        public int Id { get; set; } 



        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }


        public DateTime PurchasedOn { get; set; }
    }
}
