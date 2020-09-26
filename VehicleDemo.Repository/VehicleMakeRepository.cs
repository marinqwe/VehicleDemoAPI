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
using VehicleDemo.Repository.Mapper;
using AutoMapper.QueryableExtensions;
using System.Collections;
using System.Collections.Generic;

namespace VehicleDemo.Repository
{
    public class VehicleMakeRepository : GenericRepository<VehicleMakeEntityModel>, IVehicleMakeRepository
    {
        private readonly VehicleContext _context;
        private readonly DbSet<VehicleMakeEntityModel> dbSet;
        private readonly IMapper iMapper;

        public VehicleMakeRepository(VehicleContext context, IMapper mapper) : base(context)
        {
            _context = context;
            dbSet = _context.Set<VehicleMakeEntityModel>();
            iMapper = mapper;
        }
        public async Task<IEnumerable<IVehicleMake>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IQueryable<VehicleMakeEntityModel> vehicles = dbSet;


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


            return await vehicles.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ProjectTo<VehicleMake>(MapperConfig.CreateMapperConfig()).ToListAsync();
        }
        public async Task<IVehicleMake> FindById(object id)
        {
            VehicleMakeEntityModel vehicleEntity = await dbSet.FindAsync(id);
            IVehicleMake vehicleMake = iMapper.Map<VehicleMake>(vehicleEntity);
            return vehicleMake;
        }
        public async Task<bool> Create(IVehicleMake vehicleMake)
        {
            try
            {
                VehicleMakeEntityModel entity = iMapper.Map<VehicleMakeEntityModel>(vehicleMake);
                dbSet.Add(entity);
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

                DbEntityEntry dbEntityEntry = _context.Entry(vehicleEntity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    dbSet.Attach(vehicleEntity);
                }
                dbEntityEntry.State = EntityState.Modified;
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

                DbEntityEntry dbEntityEntry = _context.Entry(vehicleEntity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    dbSet.Attach(vehicleEntity);
                    dbSet.Remove(vehicleEntity);
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

                VehicleMakeEntityModel entity = await dbSet.FindAsync(id);
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
