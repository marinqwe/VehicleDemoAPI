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
using VehicleDemo.Repository.Common;

namespace VehicleDemo.Repository
{
    public class VehicleModelRepository : GenericRepository<VehicleModel>, IVehicleModelRepository
    {
        private readonly VehicleContext _context;
        private readonly DbSet<VehicleModel> dbSet;
        public VehicleModelRepository(VehicleContext context) : base(context)
        {
            _context = context;
            dbSet = _context.Set<VehicleModel>();
        }
        public override async Task<IQueryable<VehicleModel>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            IQueryable<VehicleModel> models = dbSet;

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
            return models.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage);
        }
        public override async Task<VehicleModel> FindById(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public override async Task<bool> Create(VehicleModel entity)
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
        public override async Task<bool> Edit(VehicleModel entity)
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
        public override void Delete(VehicleModel entityToDelete)
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
                VehicleModel entity = await dbSet.FindAsync(id);
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
