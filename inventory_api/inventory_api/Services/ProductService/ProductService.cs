using inventory_api.Data;
using inventory_api.Models;

using Microsoft.EntityFrameworkCore;

using System.Runtime.Serialization;

namespace inventory_api.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await (

                from p in _context.Products
                join s in _context.Suppliers on p.SupplierId equals s.SupplierId
                join c in _context.Categories on p.CategoryId equals c.CategoryId
                select new Product
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    UnitPrice = p.UnitPrice,
                    SupplierId = p.SupplierId,
                    StockQuantity = p.StockQuantity,
                    CategoryId = p.CategoryId,
                    Supplier = s,
                    Category = c
                }).ToListAsync();


            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                throw new ArgumentException("Id mismatch");
            }
            
            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.SupplierId = product.SupplierId;

            _context.Entry(existingProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    throw new NotFoundException($"Product with id {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return existingProduct;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new NotFoundException($"Product with id {id} not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }

    [Serializable]
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string? message) : base(message)
        {
        }

        public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
