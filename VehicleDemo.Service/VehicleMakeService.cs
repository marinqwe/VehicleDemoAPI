using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;
using VehicleDemo.Repository.Common;
using VehicleDemo.Service.Common;

namespace VehicleDemo.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IUnitOfWork uow;

        public VehicleMakeService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public async Task<IEnumerable<IVehicleMake>> GetVehicleMakes(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            Func<IQueryable<VehicleMake>, IOrderedQueryable<VehicleMake>> sortBy = null;
            Expression<Func<VehicleMake, bool>> filter = null;

            if (filters.ShouldApplyFilters())
            {
                filter = (v => v.Name.Contains(filters.FilterBy) || v.Abrv.Contains(filters.FilterBy));
            }

            switch (sorting.SortBy)
            {
                case "name_desc":
                    sortBy = q => q.OrderByDescending(v => v.Name);
                    break;

                case "abrv":
                    sortBy = q => q.OrderBy(v => v.Abrv);
                    break;

                case "abrv_desc":
                    sortBy = q => q.OrderByDescending(v => v.Abrv);
                    break;

                default:
                    sortBy = q => q.OrderBy(v => v.Name);
                    break;
            }

            IQueryable<IVehicleMake> query = await uow.VehicleMakes.GetAll(filter: filter, orderBy: sortBy);
            paging.TotalCount = query.Count();

            return query.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ToList();
        }

        public async Task<IVehicleMake> FindVehicleMake(object id)
        {
            return await uow.VehicleMakes.FindById(id);
        }

        public async Task<bool> CreateVehicleMake(IVehicleMake vehicleMake)
        {
            VehicleMake vehicleToCreate = new VehicleMake
            {
                Name = vehicleMake.Name,
                Abrv = vehicleMake.Abrv,
                MakeId = vehicleMake.MakeId
            };
            try
            {
                await uow.VehicleMakes.Create(vehicleToCreate);
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
            VehicleMake vehicleToEdit = new VehicleMake
            {
                Name = vehicleMake.Name,
                Abrv = vehicleMake.Abrv,
                MakeId = vehicleMake.MakeId
            };
            try
            {
                await uow.VehicleMakes.Edit(vehicleToEdit);
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
                await uow.VehicleMakes.Delete(id);
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