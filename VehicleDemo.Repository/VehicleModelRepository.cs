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
using VehicleDemo.Repository.Mapper;

namespace VehicleDemo.Repository
{
    public class VehicleModelRepository : GenericRepository<VehicleModelEntityModel>, IVehicleModelRepository
    {
        private readonly VehicleContext _context;
        private readonly DbSet<VehicleModelEntityModel> dbSet;
        private readonly IMapper iMapper;
        public VehicleModelRepository(VehicleContext context, IMapper mapper) : base(context)
        {
            _context = context;
            dbSet = _context.Set<VehicleModelEntityModel>();
            iMapper = mapper;
        }
        public async Task<IEnumerable<IVehicleModel>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IQueryable<VehicleModelEntityModel> models = dbSet;

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
            return await models.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ProjectTo<VehicleModel>(MapperConfig.CreateMapperConfig()).ToListAsync();
        }
        public async Task<IVehicleModel> FindById(object id)
        {
            VehicleModelEntityModel entity = await dbSet.FindAsync(id);
            IVehicleModel model = iMapper.Map<VehicleModel>(entity);
            return model;
        }
        public async Task<bool> Create(IVehicleModel vehicleModel)
        {
            try
            {
                VehicleModelEntityModel entity = iMapper.Map<VehicleModelEntityModel>(vehicleModel);
                dbSet.Add(entity);
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
                DbEntityEntry dbEntityEntry = _context.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
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
                DbEntityEntry dbEntityEntry = _context.Entry(entityToDelete);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    dbSet.Attach(entityToDelete);
                    dbSet.Remove(entityToDelete);
                }
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
                VehicleModelEntityModel entity = await dbSet.FindAsync(id);
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
