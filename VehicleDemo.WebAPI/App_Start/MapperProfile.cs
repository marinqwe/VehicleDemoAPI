using AutoMapper;
using VehicleDemo.Model;
using VehicleDemo.WebAPI.ViewModels;

namespace VehicleDemo.WebAPI.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
        }
    }
}