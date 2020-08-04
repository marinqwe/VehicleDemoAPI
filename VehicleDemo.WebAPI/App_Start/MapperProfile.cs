using AutoMapper;
using VehicleDemo.Model;
using VehicleDemo.WebAPI.ViewModels;

namespace VehicleDemo.WebAPI.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>();
            CreateMap<VehicleModel, VehicleModelViewModel>();
        }
    }
}