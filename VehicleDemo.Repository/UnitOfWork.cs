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
        private IGenericRepository<VehicleMake> _vehicleMakeRepository;
        private IGenericRepository<VehicleModel> _vehicleModelRepository;

        public UnitOfWork()
        {
            _context = new VehicleContext();
        }
        public UnitOfWork(VehicleContext context, IGenericRepository<VehicleMake> vehicleMakeRepository, IGenericRepository<VehicleModel> vehicleModelRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _vehicleMakeRepository = vehicleMakeRepository;
            _vehicleModelRepository = vehicleModelRepository;
        }
        public IGenericRepository<VehicleMake> VehicleMakes => _vehicleMakeRepository ?? (_vehicleMakeRepository = new GenericRepository<VehicleMake>(_context));
        public IGenericRepository<VehicleModel> VehicleModels => _vehicleModelRepository ?? (_vehicleModelRepository = new GenericRepository<VehicleModel>(_context));
        public async Task SaveChangesAsync()
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _context.SaveChangesAsync();
                scope.Complete();
            }
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
