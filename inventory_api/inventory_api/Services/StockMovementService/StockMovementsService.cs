using inventory_api.Data;
using inventory_api.Models;
using Microsoft.EntityFrameworkCore;

using System.Runtime.Serialization;

namespace inventory_api.Services.StockMovementService
{
    public class StockMovementsService: IStockMovementsService
    {
        private readonly ApplicationDbContext _context;

        public StockMovementsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockMovements>> GetStockMovements()
        {
            //return await _context.StockMovements.Include(s => s.Product).ToListAsync();
            var stockMovements = await (

                from sm in _context.StockMovements
                join p in _context.Products on sm.ProductId equals p.ProductId
                join ot in _context.OrderTypes on sm.OrderTypeId equals ot.OrderTypeId
                select new StockMovements
                {
                    StockMovementsId = sm.StockMovementsId,
                    Date = sm.Date,
                    Quantity = sm.Quantity,
                    OrderTypeId = sm.OrderTypeId,
                    ProductId = sm.ProductId,
                    OrderType = sm.OrderType,
                    Product = sm.Product,
                }).ToListAsync();

            return stockMovements;
        }

        public async Task<StockMovements> GetStockMovement(int id)
        {
            return await _context.StockMovements.FindAsync(id);
        }

        public async Task<StockMovements> CreateStockMovement(StockMovements stockMovement)
        {
            _context.StockMovements.Add(stockMovement);
            await _context.SaveChangesAsync();

            return stockMovement;
        }

        public async Task<StockMovements> UpdateStockMovement(int id, StockMovements stockMovement)
        {
            var existingStoveMovement = await _context.StockMovements.FindAsync(id);

            if (existingStoveMovement == null)
            {
                throw new ArgumentException("Id mismatch");
            }

            existingStoveMovement.Date = stockMovement.Date;
            existingStoveMovement.Quantity = stockMovement.Quantity;
            existingStoveMovement.OrderTypeId = stockMovement.OrderTypeId;
            existingStoveMovement.ProductId = stockMovement.ProductId;

            _context.Entry(existingStoveMovement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockMovementExists(id))
                {
                    throw new NotFoundException($"StockMovement with id {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return existingStoveMovement;
        }

        public async Task DeleteStockMovement(int id)
        {
            var stockMovement = await _context.StockMovements.FindAsync(id);

            if (stockMovement == null)
            {
                throw new NotFoundException($"StockMovement with id {id} not found");
            }

            _context.StockMovements.Remove(stockMovement);
            await _context.SaveChangesAsync();
        }

        private bool StockMovementExists(int id)
        {
            return _context.StockMovements.Any(e => e.StockMovementsId == id);
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
