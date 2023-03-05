using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using inventory_api.Data;
using inventory_api.Models;
using inventory_api.Services.StockMovementService;

namespace inventory_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : ControllerBase
    {
        private readonly IStockMovementsService _stockMovementsService;

        public StockMovementsController(IStockMovementsService stockMovementsService)
        {
            _stockMovementsService = stockMovementsService;
        }

        // GET: api/StockMovements
        [HttpGet]
        public async Task<IEnumerable<StockMovements>> GetStockMovements()
        {
            return await _stockMovementsService.GetStockMovements();
        }

        // GET: api/StockMovements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockMovements>> GetStockMovement(int id)
        {
            var stockMovements = await _stockMovementsService.GetStockMovement(id);

            if (stockMovements == null)
            {
                return NotFound();
            }

            return stockMovements;
        }

        // PUT: api/StockMovements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<StockMovements>> UpdateStockMovement(int id, StockMovements stockMovement)
        {
            try
            {
                var updatedStockMovement = await _stockMovementsService.UpdateStockMovement(id, stockMovement);

                return updatedStockMovement;
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

        // POST: api/StockMovements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StockMovements>> CreateStockMovements(StockMovements stockMovements)
        {
            var createdStockMovement = await _stockMovementsService.CreateStockMovement(stockMovements);

            return CreatedAtAction("GetStockMovements", new { id = stockMovements.StockMovementsId }, createdStockMovement);
        }

        // DELETE: api/StockMovements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockMovement(int id)
        {
            try
            {
                await _stockMovementsService.DeleteStockMovement(id);

                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
