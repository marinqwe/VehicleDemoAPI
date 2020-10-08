using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VehicleDemo.DAL;
using VehicleDemo.Model;
using VehicleDemo.Common.Helpers;
using System.Data.Entity.Infrastructure;
using VehicleDemo.Repository.Common;
using VehicleDemo.Model.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections;
using System.Collections.Generic;

namespace VehicleDemo.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private readonly IGenericRepository<VehicleMakeEntityModel> _genericRepository;
        private readonly IMapper iMapper;
        private readonly MapperConfiguration _mapperConfiguration;

        public VehicleMakeRepository(IMapper mapper, MapperConfiguration mapperConfiguration, IGenericRepository<VehicleMakeEntityModel> genericRepository )
        {
            _genericRepository = genericRepository;
            iMapper = mapper;
            _mapperConfiguration = mapperConfiguration;
        }
        public async Task<IEnumerable<IVehicleMake>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IQueryable<VehicleMakeEntityModel> vehicles = _genericRepository.GetAll();


            if (filters.ShouldApplyFilters())
            {
                vehicles = vehicles.Where(m => m.Name.Contains(filters.FilterBy) || m.Abrv.Contains(filters.FilterBy));
            }

            paging.TotalCount = vehicles.Count();
            switch (sorting.SortBy)
            {
                case "name_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    vehicles = vehicles.OrderBy(v => v.Abrv);
                    break;

                case "abrv_desc":
                    vehicles = vehicles.OrderByDescending(v => v.Abrv);
                    break;

                default:
                    vehicles = vehicles.OrderBy(v => v.Name);
                    break;
            }


            return await vehicles.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ProjectTo<VehicleMake>(_mapperConfiguration).ToListAsync();
        }
        public async Task<IVehicleMake> FindById(object id)
        {
            VehicleMakeEntityModel vehicleEntity = await _genericRepository.FindById(id);
            IVehicleMake vehicleMake = iMapper.Map<VehicleMake>(vehicleEntity);
            return vehicleMake;
        }
        public async Task<bool> Create(IVehicleMake vehicleMake)
        {
            try
            {
                VehicleMakeEntityModel entity = iMapper.Map<VehicleMakeEntityModel>(vehicleMake);
                await _genericRepository.Create(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Edit(IVehicleMake entity)
        {
            try
            {
                VehicleMakeEntityModel vehicleEntity = iMapper.Map<VehicleMakeEntityModel>(entity);

                await _genericRepository.Edit(vehicleEntity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Delete(IVehicleMake entityToDelete)
        {
            try
            {
                VehicleMakeEntityModel vehicleEntity = iMapper.Map<VehicleMakeEntityModel>(entityToDelete);

                _genericRepository.Delete(vehicleEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Delete(object id)
        {
            try
            {
                VehicleMakeEntityModel entity = await _genericRepository.FindById(id);
                await Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
