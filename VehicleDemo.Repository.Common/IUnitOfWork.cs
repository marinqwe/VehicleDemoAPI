using System;
using System.Threading.Tasks;
using VehicleDemo.Model;

namespace VehicleDemo.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<VehicleMake> VehicleMakes { get; }
        IGenericRepository<VehicleModel> VehicleModels { get; }
        Task SaveChangesAsync();
        new void Dispose();
    }
}
