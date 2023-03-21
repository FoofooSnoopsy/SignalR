using System.ComponentModel.DataAnnotations;

namespace SingalR.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(10)]
        public string? Gender { get; set; }

        public string? Mobile { get; set; } 

    }
}
