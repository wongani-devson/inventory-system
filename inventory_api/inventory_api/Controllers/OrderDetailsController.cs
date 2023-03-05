using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_api.Data;
using inventory_api.Models;
using inventory_api.Services.OrderDetailsService;
using inventory_api.Services.OrderDetials;

namespace inventory_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsService _orderDetailsService;

        public OrderDetailsController(IOrderDetailsService orderDetailsService)
        {
            _orderDetailsService = orderDetailsService;
        }

        
        [HttpGet]
        public async Task<IEnumerable<OrderDetails>> GetOrderDetials()
        {
            return await _orderDetailsService.GetOrderDetails();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetail(int id)
        {
            var orderDetail = await _orderDetailsService.GetOrderDetail(id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDetails>> UpdateOrderDetail(int id, OrderDetails orderDetail)
        {
            try
            {
                var updatedOrderDetail = await _orderDetailsService.UpdateOrderDetail(id, orderDetail);

                return updatedOrderDetail;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> CreateOrderDetail(OrderDetails orderDetail)
        {
            var createdOrderDetail = await _orderDetailsService.CreateOrderDetail(orderDetail);

            return CreatedAtAction(nameof(GetOrderDetail), new { id = createdOrderDetail.OrderDetailsId }, createdOrderDetail);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            try
            {
                await _orderDetailsService.DeleteOrderDetail(id);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
