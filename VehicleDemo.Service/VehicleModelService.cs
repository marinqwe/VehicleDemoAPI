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
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IUnitOfWork uow;

        public VehicleModelService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public async Task<IEnumerable<IVehicleModel>> GetVehicleModels(VehicleFilters filters, VehicleSorting sorting, VehiclePaging paging)
        {
            Func<IQueryable<VehicleModel>, IOrderedQueryable<VehicleModel>> sortBy = null;
            Expression<Func<VehicleModel, bool>> filter = null;

            if (filters.ShouldApplyFilters())
            {
                filter = (m => m.Name.Contains(filters.FilterBy)
                                    || m.Abrv.Contains(filters.FilterBy)
                                    || m.MakeId.ToString().Contains(filters.FilterBy));
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

                case "makeId":
                    sortBy = q => q.OrderBy(v => v.MakeId);
                    break;

                case "makeId_desc":
                    sortBy = q => q.OrderByDescending(v => v.MakeId);
                    break;

                default:
                    sortBy = q => q.OrderBy(v => v.Name);
                    break;
            }
            IQueryable<IVehicleModel> query = await uow.VehicleModels.GetAll(filter: filter, orderBy: sortBy);
            paging.TotalCount = query.Count();
            return query.Skip(paging.ItemsToSkip).Take(paging.ResultsPerPage).ToList();
        }

        public async Task<IVehicleModel> FindVehicleModel(object id)
        {
            return await uow.VehicleModels.FindById(id);
        }

        public async Task<bool> CreateVehicleModel(IVehicleModel vehicleModel)
        {
            VehicleModel model = new VehicleModel()
            {
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                MakeId = vehicleModel.MakeId,
                Id = vehicleModel.Id
            };

            try
            {
                await uow.VehicleModels.Create(model);
                await uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditVehicleModel(IVehicleModel vehicleModel)
        {
            VehicleModel model = new VehicleModel()
            {
                Name = vehicleModel.Name,
                Abrv = vehicleModel.Abrv,
                MakeId = vehicleModel.MakeId,
                Id = vehicleModel.Id
            };
            try
            {
                await uow.VehicleModels.Edit(model);
                await uow.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteVehicleModel(object id)
        {
            try
            {
                await uow.VehicleModels.Delete(id);
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