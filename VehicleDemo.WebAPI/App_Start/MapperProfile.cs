﻿using AutoMapper;
using System.Linq;
using VehicleDemo.DAL;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;
using VehicleDemo.WebAPI.ViewModels;

namespace VehicleDemo.WebAPI.App_Start
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
            CreateMap<VehicleMake, VehicleMakeEntityModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelEntityModel>().ReverseMap();
        }
    }
}