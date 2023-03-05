using System.ComponentModel.DataAnnotations;

namespace inventory_api.Models
{
    public class Category
    {
        [Key]
        public int CategoryId  { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

    }
}
