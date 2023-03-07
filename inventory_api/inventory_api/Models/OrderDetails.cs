using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_api.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailsId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
    }
}
