using inventory_api.Data;
using inventory_api.Models;
using Microsoft.EntityFrameworkCore;

using System.Runtime.Serialization;

namespace inventory_api.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            //return await _context.Orders.Include(p => p.Customer).ToListAsync();
            var Orders = await (

                from o in _context.Orders
                join c in _context.Customers on o.CustomerId equals c.CustomerId
                join od in _context.OrderDetails on o.OrderDetailsId equals od.OrderDetailsId

                select new Order
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalCost = o.TotalCost,
                    CustomerId = o.CustomerId,
                    Customer = o.Customer,
                    OrderDetailsId = od.OrderDetailsId,
                    OrderDetails = o.OrderDetails,
                }).ToListAsync();

            return Orders;
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> CreateOrder(Order Order)
        {
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return Order;
        }

        public async Task<Order> UpdateOrder(int id, Order Order)
        {
            var existingOrder = await _context.Orders.FindAsync(id);

            if (existingOrder == null)
            {
                throw new ArgumentException("Id mismatch");
            }

            existingOrder.OrderDate = Order.OrderDate;
            existingOrder.TotalCost = Order.TotalCost;
            existingOrder.CustomerId = Order.CustomerId;
            existingOrder.OrderDetailsId = Order.OrderDetailsId;

            _context.Entry(existingOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    throw new NotFoundException($"Order with id {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return existingOrder;
        }

        public async Task DeleteOrder(int id)
        {
            var Order = await _context.Orders.FindAsync(id);

            if (Order == null)
            {
                throw new NotFoundException($"Order with id {id} not found");
            }

            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
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
