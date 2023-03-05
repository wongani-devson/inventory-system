using System.ComponentModel.DataAnnotations;

namespace inventory_api.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? ContactInformation { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
