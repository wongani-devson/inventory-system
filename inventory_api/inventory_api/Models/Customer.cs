using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_api.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactInformatin { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalSpend { get; set; }

        public virtual ICollection<Order>? Orders { get; set;}
    }
}
