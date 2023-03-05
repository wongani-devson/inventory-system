using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Services.SupplierService
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetSupplier(int id);
        Task<Supplier> CreateSupplier(Supplier supplier);
        Task<Supplier> UpdateSupplier(int id, Supplier supplier);
        Task DeleteSupplier(int id);

    }
}
