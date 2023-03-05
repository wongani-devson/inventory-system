using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_api.Models
{
    public class StockMovements
    {
        [Key]
        public int StockMovementsId { get; set; }
        public DateTime Date { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }

        [ForeignKey("OrderType")]
        public int OrderTypeId { get; set; }
        public OrderType OrderType { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
