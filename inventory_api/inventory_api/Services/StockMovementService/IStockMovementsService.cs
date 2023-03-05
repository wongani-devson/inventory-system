using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Services.StockMovementService
{
    public interface IStockMovementsService
    {
        Task<IEnumerable<StockMovements>> GetStockMovements();
        Task<StockMovements> UpdateStockMovement(int id, StockMovements stockMovements);
        Task<StockMovements> GetStockMovement(int id);
        Task<StockMovements> CreateStockMovement(StockMovements stockMovements);
        Task DeleteStockMovement(int id);

    }
}
