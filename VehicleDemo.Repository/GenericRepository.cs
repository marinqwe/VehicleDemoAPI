using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.DAL;

namespace VehicleDemo.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly VehicleContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(VehicleContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> FindById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<bool> Create(T entity)
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

        public virtual async Task<bool> Edit(T entity)
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
        public virtual void Delete(T entityToDelete)
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
        public virtual async Task<bool> Delete(object id)
        {
            try
            {
                T entity = await dbSet.FindAsync(id);
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
