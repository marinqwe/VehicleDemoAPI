using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VehicleDemo.Common.Helpers;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;
using VehicleDemo.Service.Common;
using VehicleDemo.WebAPI.ViewModels;

namespace VehicleDemo.WebAPI.Controllers
{
    public class VehicleModelController : ApiController
    {
        private readonly IMapper iMapper;
        private readonly IVehicleModelService _vehicleService;

        public VehicleModelController()
        {
        }

        public VehicleModelController(IVehicleModelService vehicleService, IMapper mapper)
        {
            _vehicleService = vehicleService;
            iMapper = mapper;
        }
        public async Task<IHttpActionResult> GetVehicleModels(string sortBy, string searchString, int? page)
        {
            VehicleFilters filters = new VehicleFilters(searchString);
            VehicleSorting sorting = new VehicleSorting(sortBy);
            VehiclePaging paging = new VehiclePaging(page);

            IEnumerable<IVehicleModel> vehicleModels = await _vehicleService.GetVehicleModels(filters, sorting, paging);

            List<VehicleModelViewModel> vehicleModelsDest = iMapper.Map<List<VehicleModelViewModel>>(vehicleModels);
            return Ok(new
            {
                models = vehicleModelsDest,
                pagingInfo = new
                {
                    resultsPerPage = paging.ResultsPerPage,
                    totalCount = paging.TotalCount,
                    pageNumber = paging.Page,
                }
            });
        }

        [ResponseType(typeof(VehicleModelViewModel))]
        public async Task<IHttpActionResult> GetVehicleModel(int id)
        {
            IVehicleModel vehicleModel = await _vehicleService.FindVehicleModel(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            VehicleModelViewModel vehicleModelViewModel = iMapper.Map<VehicleModelViewModel>(vehicleModel);
            return Ok(vehicleModelViewModel);
        }


        [ResponseType(typeof(VehicleModelViewModel))]
        public async Task<IHttpActionResult> CreateVehicleModel(VehicleModelViewModel vehicleModelViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleModel vehicleModel = iMapper.Map<VehicleModel>(vehicleModelViewModel);
            await _vehicleService.CreateVehicleModel(vehicleModel);

            return CreatedAtRoute("DefaultApi", new { id = vehicleModelViewModel.Id }, vehicleModelViewModel);
        }

        [ResponseType(typeof(VehicleModelViewModel))]
        public async Task<IHttpActionResult> PutVehicleModel(int id, VehicleModelViewModel vehicleModelViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleModelViewModel.Id)
            {
                return BadRequest();
            }
            VehicleModel vehicleModel = iMapper.Map<VehicleModel>(vehicleModelViewModel);
            await _vehicleService.EditVehicleModel(vehicleModel);

            return Ok(vehicleModelViewModel);
        }

        [ResponseType(typeof(void))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteVehicleModel(int id)
        {
            IVehicleModel vehicleModel = await _vehicleService.FindVehicleModel(id);

            if (vehicleModel == null)
            {
                return NotFound();
            }

            await _vehicleService.DeleteVehicleModel(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}