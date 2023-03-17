using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SingalR.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar"), StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName ="nvarchar"), StringLength(10)]
        public string? Gender { get; set; }

        [Column(TypeName ="nvarchar"), StringLength(10)]
        public string? Mobile { get; set; }
    }
}
