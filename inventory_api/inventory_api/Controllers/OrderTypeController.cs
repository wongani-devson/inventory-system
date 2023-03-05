using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_api.Data;
using inventory_api.Models;
using inventory_api.Services.OrderTypeService;

namespace inventory_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTypeController : ControllerBase
    {
        private readonly ILogger<OrderTypeController> _logger;
        private readonly IOrderTypeService _oderTypeService;

        public OrderTypeController(ILogger<OrderTypeController> logger, IOrderTypeService oderTypeService)
        {
            _logger = logger;
            _oderTypeService = oderTypeService;
        }

        // GET: api/OrderTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderType>>> GetOrderTypes()
        {
            return await _oderTypeService.GetOrderTypes();
        }
    }
}
