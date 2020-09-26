using AutoMapper;
using System.Linq;
using VehicleDemo.DAL;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;

namespace VehicleDemo.Repository.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeEntityModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelEntityModel>().ReverseMap();
        }
    }
}
