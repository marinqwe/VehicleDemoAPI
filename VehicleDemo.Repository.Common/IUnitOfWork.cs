using System;
using System.Threading.Tasks;
using VehicleDemo.Model;

namespace VehicleDemo.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
        new void Dispose();
    }
}
