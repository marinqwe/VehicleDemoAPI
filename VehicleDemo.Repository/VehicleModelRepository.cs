using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.DAL;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;
using VehicleDemo.Repository.Common;

namespace VehicleDemo.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly IGenericRepository<VehicleModelEntityModel> _genericRepository;
        private readonly IMapper iMapper;
        private readonly MapperConfiguration _mapperConfiguration;
        public VehicleModelRepository(IMapper mapper, MapperConfiguration mapperConfiguration, IGenericRepository<VehicleModelEntityModel> genericRepository)
        {
            _genericRepository = genericRepository;
            iMapper = mapper;
            _mapperConfiguration = mapperConfiguration;
        }
        public async Task<IEnumerable<IVehicleModel>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IQueryable<VehicleModelEntityModel> models = _genericRepository.GetAll();

            if (filters.ShouldApplyFilters())
            {
                models = models.Where(m => m.Name.Contains(filters.FilterBy)
                                    || m.Abrv.Contains(filters.FilterBy)
                                    || m.MakeId.ToString().Contains(filters.FilterBy));
            }

            paging.TotalCount = models.Count();

            switch (sorting.SortBy)
            {
                case "name_desc":
                    models = models.OrderByDescending(v => v.Name);
                    break;

                case "Abrv":
                    models = models.OrderBy(v => v.Abrv);
                    break;

                case "abrv_desc":
                    models = models.OrderByDescending(v => v.Abrv);
                    break;

                case "MakeId":
                    models = models.OrderBy(v => v.MakeId);
                    break;

                case "makeid_desc":
                    models = models.OrderByDescending(v => v.MakeId);
                    break;

                default:
                    models = models.OrderBy(v => v.Name);
                    break;
            }
            return await models.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ProjectTo<VehicleModel>(_mapperConfiguration).ToListAsync();
        }
        public async Task<IVehicleModel> FindById(object id)
        {
            VehicleModelEntityModel entity = await _genericRepository.FindById(id);
            IVehicleModel model = iMapper.Map<VehicleModel>(entity);
            return model;
        }
        public async Task<bool> Create(IVehicleModel vehicleModel)
        {
            try
            {
                VehicleModelEntityModel entity = iMapper.Map<VehicleModelEntityModel>(vehicleModel);
                await _genericRepository.Create(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Edit(IVehicleModel vehicleModel)
        {
            try
            {
                VehicleModelEntityModel entity = iMapper.Map<VehicleModelEntityModel>(vehicleModel);
                await _genericRepository.Edit(entity);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Delete(IVehicleModel vehicleModel)
        {
            try
            {
                VehicleModelEntityModel entityToDelete = iMapper.Map<VehicleModelEntityModel>(vehicleModel);
                _genericRepository.Delete(entityToDelete);
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
                VehicleModelEntityModel entity = await _genericRepository.FindById(id);
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
