using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;
using VehicleDemo.Repository;
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

            IQueryable<IVehicleModel> query = await _repository.GetAll(filters, sorting, paging);

            return query.ToList();
        }

        public async Task<IVehicleModel> FindVehicleModel(object id)
        {
            return await _repository.FindById(id);
        }

        public async Task<bool> CreateVehicleModel(IVehicleModel vehicleModel)
        {
            VehicleModel model = new VehicleModel()
            {
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                MakeId = vehicleModel.MakeId,
                Id = vehicleModel.Id
            };

            try
            {
                await _repository.Create(model);
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
            VehicleModel model = new VehicleModel()
            {
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                MakeId = vehicleModel.MakeId,
                Id = vehicleModel.Id
            };
            try
            {
                await _repository.Edit(model);
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