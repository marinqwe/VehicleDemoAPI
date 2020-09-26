using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDemo.Repository.Mapper
{
    public static class MapperConfig
    {
        public static MapperConfiguration CreateMapperConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            return config;
        }
    }
}
