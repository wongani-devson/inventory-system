using inventory_api.Data;
using inventory_api.Models;
using inventory_api.Services.OrderDetailsService;

using Microsoft.EntityFrameworkCore;

using System.Runtime.Serialization;

namespace inventory_api.Services.OrderDetials
{
    public class OrderDetailsService : IOrderDetailsService
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetails()
        {
            //return await _context.OrderDetails.Include(o => o.Product)
            //    .Include(p => p.Order).ToListAsync()
            var OrderDetails = await (

                from od in _context.OrderDetails
                join p in _context.Products on od.ProductId equals p.ProductId
                select new OrderDetails
                {
                    OrderDetailsId = od.OrderDetailsId,
                    Quantity = od.Quantity,
                    Product = p
                }).ToListAsync();

            return OrderDetails;

        }

        public async Task<OrderDetails> GetOrderDetail(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task<OrderDetails> CreateOrderDetail(OrderDetails OrderDetail)
        {
            _context.OrderDetails.Add(OrderDetail);
            await _context.SaveChangesAsync();

            return OrderDetail;
        }

        public async Task<OrderDetails> UpdateOrderDetail(int id, OrderDetails OrderDetail)
        {
            var existingOrderDetails = await _context.OrderDetails.FindAsync(id);

            if (existingOrderDetails == null)
            {
                throw new ArgumentException("Id mismatch");
            }

            existingOrderDetails.Quantity = OrderDetail.Quantity;
            existingOrderDetails.ProductId = OrderDetail.ProductId;

            _context.Entry(existingOrderDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
                {
                    throw new NotFoundException($"OrderDetail with id {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return existingOrderDetails;
        }

        public async Task DeleteOrderDetail(int id)
        {
            var OrderDetails = await _context.OrderDetails.FindAsync(id);

            if (OrderDetails == null)
            {
                throw new NotFoundException($"OrderDetails with id {id} not found");
            }

            _context.OrderDetails.Remove(OrderDetails);
            await _context.SaveChangesAsync();
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailsId == id);
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
