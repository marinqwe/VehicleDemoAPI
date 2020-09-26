using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model.Common;
using VehicleDemo.Repository.Common;
using VehicleDemo.Service.Common;

namespace VehicleDemo.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IUnitOfWork uow;
        private readonly IVehicleModelRepository _repository;

        public VehicleModelService(IUnitOfWork unitOfWork, IVehicleModelRepository repository)
        {
            uow = unitOfWork;
            _repository = repository;
        }

        public async Task<IEnumerable<IVehicleModel>> GetVehicleModels(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {

            IEnumerable<IVehicleModel> query = await _repository.GetAll(filters, sorting, paging);

            return query.ToList();
        }

        public async Task<IVehicleModel> FindVehicleModel(object id)
        {
            return await _repository.FindById(id);
        }

        public async Task<bool> CreateVehicleModel(IVehicleModel vehicleModel)
        {
            try
            {
                await _repository.Create(vehicleModel);
                await uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditVehicleModel(IVehicleModel vehicleModel)
        {
            try
            {
                await _repository.Edit(vehicleModel);
                await uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehicleModel(object id)
        {
            try
            {
                await _repository.Delete(id);
                await uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            uow.Dispose();
        }
    }
}