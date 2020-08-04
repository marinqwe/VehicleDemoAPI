using AutoMapper;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;
using VehicleDemo.Repository;
using VehicleDemo.Repository.Common;
using VehicleDemo.WebAPI.Controllers;

namespace VehicleDemo.WebAPI.App_Start
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));

        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperProfile>();
            });

            return config;
        }
    }
}