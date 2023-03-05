using inventory_api.Data;
using inventory_api.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory_api.Services.OrderTypeService
{
    public class OrderTypeService: IOrderTypeService
    {
        private readonly ApplicationDbContext _context;

        public OrderTypeService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<OrderType>>> GetOrderTypes()
        {
            return await _context.OrderTypes.ToListAsync();
        }
    }
}
