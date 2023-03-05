using System.ComponentModel.DataAnnotations;


namespace inventory_api.Models
{
    public class OrderType
    {
        [Key]
        public int OrderTypeId { get; set; }
        public string? OrderTypeName { get; set; }

        public virtual ICollection<StockMovements>? StockMovements { get; set; }
    }

}
