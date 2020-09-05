using System.Linq;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;

namespace VehicleDemo.Repository.Common
{
    public interface IVehicleModelRepository
    {
        Task<IQueryable<VehicleModel>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);
        Task<VehicleModel> FindById(object id);
        Task<bool> Create(VehicleModel entity);
        Task<bool> Edit(VehicleModel entity);
        void Delete(VehicleModel entityToDelete);
        Task<bool> Delete(object id);
    }
}
