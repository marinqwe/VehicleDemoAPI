using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;

namespace VehicleDemo.Repository.Common
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<IVehicleModel>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);
        Task<IVehicleModel> FindById(object id);
        Task<bool> Create(IVehicleModel entity);
        Task<bool> Edit(IVehicleModel entity);
        void Delete(IVehicleModel entityToDelete);
        Task<bool> Delete(object id);
    }
}
