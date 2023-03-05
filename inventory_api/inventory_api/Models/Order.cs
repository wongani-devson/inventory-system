using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_api.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? TotalCost { get; set; }

        [ForeignKey("CustomerId")]
        public  int CustomerId { get; set; }
        public  Customer? Customer { get; set; }

    }
}
