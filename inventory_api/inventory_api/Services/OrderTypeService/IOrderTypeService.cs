using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Services.OrderTypeService
{
    public interface IOrderTypeService
    {
        public  Task<ActionResult<IEnumerable<OrderType>>> GetOrderTypes();
    }
}