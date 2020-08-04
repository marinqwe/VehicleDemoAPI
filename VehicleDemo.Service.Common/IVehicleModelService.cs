using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;

namespace VehicleDemo.Service.Common
{
    public interface IVehicleModelService
    {
        Task<IEnumerable<IVehicleModel>> GetVehicleModels(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);
        Task<IVehicleModel> FindVehicleModel(object id);

        Task<bool> CreateVehicleModel(IVehicleModel vehicleModel);

        Task<bool> EditVehicleModel(IVehicleModel vehicleModel);

        Task<bool> DeleteVehicleModel(object id);

        void Dispose();
    }
}
