using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDemo.DAL;
using VehicleDemo.Model;
using VehicleDemo.Common.Helpers;
using System.Data.Entity.Infrastructure;
using VehicleDemo.Repository.Common;

namespace VehicleDemo.Repository
{
    public class VehicleMakeRepository : GenericRepository<VehicleMake>, IVehicleMakeRepository
    {
        private readonly VehicleContext _context;
        private readonly DbSet<VehicleMake> dbSet;


        public VehicleMakeRepository(VehicleContext context) : base(context)
        {
            _context = context;
            dbSet = _context.Set<VehicleMake>();
        }
        public override async Task<IQueryable<VehicleMake>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IQueryable<VehicleMake> vehicles = dbSet;

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

            return vehicles.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage);
        }
        public override async Task<VehicleMake> FindById(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public override async Task<bool> Create(VehicleMake entity)
        {
            try
            {
                dbSet.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public override async Task<bool> Edit(VehicleMake entity)
        {
            try
            {
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
        public override void Delete(VehicleMake entityToDelete)
        {
            try
            {
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
        public override async Task<bool> Delete(object id)
        {
            try
            {
                VehicleMake entity = await dbSet.FindAsync(id);
                Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
