using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model.Common;

namespace VehicleDemo.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging);
        Task<T> FindById(object id);

        Task<bool> Create(T entity);

        Task<bool> Edit(T entity);

        Task<bool> Delete(object id);
    }
}
