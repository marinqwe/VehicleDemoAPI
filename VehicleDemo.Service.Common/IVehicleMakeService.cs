using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleDemo.Model.Common;
using VehicleDemo.Common.Helpers;

namespace VehicleDemo.Service.Common
{
    public interface IVehicleMakeService
    {
        Task<IEnumerable<IVehicleMake>> GetVehicleMakes(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);

        Task<IVehicleMake> FindVehicleMake(object id);

        Task<bool> CreateVehicleMake(IVehicleMake vehicleMake);

        Task<bool> EditVehicleMake(IVehicleMake vehicleMake);

        Task<bool> DeleteVehicleMake(object id);

        void Dispose();
    }
}
