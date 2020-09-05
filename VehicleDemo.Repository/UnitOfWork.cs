using System;
using VehicleDemo.Model;
using VehicleDemo.DAL;
using VehicleDemo.Repository.Common;
using System.Transactions;
using System.Threading.Tasks;

namespace VehicleDemo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleContext _context;
        public UnitOfWork(VehicleContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = 0;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _context.SaveChangesAsync();
                scope.Complete();
            }
            return result;
        }
        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
