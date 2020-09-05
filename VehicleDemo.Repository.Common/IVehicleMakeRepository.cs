using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;

namespace VehicleDemo.Repository.Common
{
    public interface IVehicleMakeRepository
    {
        Task<IQueryable<VehicleMake>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);
        Task<VehicleMake> FindById(object id);
        Task<bool> Create(VehicleMake entity);
        Task<bool> Edit(VehicleMake entity);
        void Delete(VehicleMake entityToDelete);
        Task<bool> Delete(object id);

    }
}
