using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SignalR2.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(10)]
        public string? Mobile { get; set; }
    }
}
