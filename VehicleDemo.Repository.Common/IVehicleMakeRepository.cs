using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;

namespace VehicleDemo.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<IEnumerable<IVehicleMake>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);
        Task<IVehicleMake> FindById(object id);
        Task<bool> Create(IVehicleMake entity);
        Task<bool> Edit(IVehicleMake entity);
        void Delete(IVehicleMake entityToDelete);
        Task<bool> Delete(object id);

    }
}
