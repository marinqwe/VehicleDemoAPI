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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IUnitOfWork uow;
        private readonly IVehicleMakeRepository _repository;

        public VehicleMakeService(IUnitOfWork unitOfWork, IVehicleMakeRepository repository)
        {
            uow = unitOfWork;
            _repository = repository;
        }

        public async Task<IEnumerable<IVehicleMake>> GetVehicleMakes(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IEnumerable<IVehicleMake> query = await _repository.GetAll(filters, sorting, paging);
            return query.ToList();
        }

        public async Task<IVehicleMake> FindVehicleMake(object id)
        {
            return await _repository.FindById(id);
        }

        public async Task<bool> CreateVehicleMake(IVehicleMake vehicleMake)
        {
            try
            {
                await _repository.Create(vehicleMake);
                await uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditVehicleMake(IVehicleMake vehicleMake)
        {
            try
            {
                await _repository.Edit(vehicleMake);
                await uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehicleMake(object id)
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