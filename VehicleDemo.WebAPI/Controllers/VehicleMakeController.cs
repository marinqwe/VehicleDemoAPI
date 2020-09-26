using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VehicleDemo.Service.Common;
using VehicleDemo.WebAPI.ViewModels;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;
using VehicleDemo.Common.Helpers;
using System.Net;

namespace VehicleDemo.WebAPI.Controllers
{
    public class VehicleMakeController : ApiController
    {

        private readonly IMapper iMapper;
        private readonly IVehicleMakeService _vehicleService;

        public VehicleMakeController()
        {
        }

        public VehicleMakeController(IMapper mapper, IVehicleMakeService vehicleService)
        {
            iMapper = mapper;
            _vehicleService = vehicleService;
        }

        // GET api/vehiclemake
        [HttpGet]
        public async Task<IHttpActionResult> GetVehicleMakes(string sortBy, string searchString, int? page)
        {
            VehicleFilters filters = new VehicleFilters(searchString);
            VehicleSorting sorting = new VehicleSorting(sortBy);
            VehiclePaging paging = new VehiclePaging(page);

            IEnumerable<IVehicleMake> vehicleMakes = await _vehicleService.GetVehicleMakes(filters, sorting, paging);

            List<VehicleMakeViewModel> vehiclesDest = iMapper.Map<List<VehicleMakeViewModel>>(vehicleMakes);
            return Ok(new
            {
                vehicles = vehiclesDest,
                pagingInfo = new
                {
                    resultsPerPage = paging.ResultsPerPage,
                    totalCount = paging.TotalCount,
                    pageNumber = paging.Page,
                }
            });
        }

        [ResponseType(typeof(VehicleMakeViewModel))]
        public async Task<IHttpActionResult> GetVehicleMake(int id)
        {
            IVehicleMake vehicleMake = await _vehicleService.FindVehicleMake(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            VehicleMakeViewModel vehicleMakeViewModel = iMapper.Map<VehicleMakeViewModel>(vehicleMake);
            return Ok(vehicleMakeViewModel);
        }

        [ResponseType(typeof(VehicleMakeViewModel))]
        public async Task<IHttpActionResult> CreateVehicleMake(VehicleMakeViewModel vehicleMakeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            VehicleMake vehicleMake = iMapper.Map<VehicleMake>(vehicleMakeViewModel);
            await _vehicleService.CreateVehicleMake(vehicleMake);

            return CreatedAtRoute("DefaultApi", new { id = vehicleMakeViewModel.MakeId }, vehicleMakeViewModel);
        }

        [ResponseType(typeof(VehicleMakeViewModel))]
        public async Task<IHttpActionResult> PutVehicleMake(int id, VehicleMakeViewModel vehicleMakeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicleMakeViewModel.MakeId)
            {
                return BadRequest();
            }
            VehicleMake vehicleMake = iMapper.Map<VehicleMake>(vehicleMakeViewModel);
            await _vehicleService.EditVehicleMake(vehicleMake);

            return Ok(vehicleMakeViewModel);
        }

        [ResponseType(typeof(void))]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteVehicleMake(int id)
        {
            IVehicleMake vehicleMake = await _vehicleService.FindVehicleMake(id);

            if (vehicleMake == null)
            {
                return NotFound();
            }

            await _vehicleService.DeleteVehicleMake(id);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}