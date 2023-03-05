using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Services.ProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(int id, Product product);
        Task DeleteProduct(int id);

    }
}
