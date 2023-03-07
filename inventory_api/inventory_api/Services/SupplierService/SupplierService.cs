using inventory_api.Data;
using inventory_api.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Runtime.Serialization;

namespace inventory_api.Services.SupplierService
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> GetSupplier(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<Supplier> CreateSupplier(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return supplier;
        }

        public async Task<Supplier> UpdateSupplier(int id, Supplier supplier)
        {
            var existingSupplier = await _context.Suppliers.FindAsync(id);

            if (existingSupplier ==null)
            {
                throw new ArgumentException("Id mismatch");
            }

            existingSupplier.SupplierName = supplier.SupplierName;
            existingSupplier.ContactInformation = supplier.ContactInformation;

            _context.Entry(existingSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
                {
                    throw new NotFoundException($"Supplier with id {id} not found");
                }
                else
                {
                    throw;
                }
            }

            return existingSupplier;
        }

        public async Task DeleteSupplier(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                throw new NotFoundException($"Supplier with id {id} not found");
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }

        private bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(e => e.SupplierId == id);
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
