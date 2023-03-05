using inventory_api.Models;

namespace inventory_api.Services.OrderDetailsService
{
    public interface IOrderDetailsService
    {
        Task<IEnumerable<OrderDetails>> GetOrderDetails();
        Task<OrderDetails> GetOrderDetail(int id);
        Task<OrderDetails> CreateOrderDetail(OrderDetails OrderDetail);
        Task<OrderDetails> UpdateOrderDetail(int id, OrderDetails OrderDetail);
        Task DeleteOrderDetail(int id);
    }
}
