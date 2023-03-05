using inventory_api.Models;

namespace inventory_api.Services.OrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        Task<Order> CreateOrder(Order Order);
        Task<Order> UpdateOrder(int id, Order Order);
        Task DeleteOrder(int id);
    }
}
