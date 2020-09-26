namespace VehicleDemo.DAL.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using VehicleDemo.Model;

    internal sealed class Configuration : DbMigrationsConfiguration<VehicleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "VehicleDemo.DAL.VehicleContext";
        }

        protected override void Seed(VehicleContext context)
        {
            var vehicles = new List<VehicleMakeEntityModel>
            {
                new VehicleMakeEntityModel{Name="Bavarian Motor Works", Abrv="BMW"},
                new VehicleMakeEntityModel{Name="Volkswagen", Abrv="VW"},
                new VehicleMakeEntityModel{Name="Ford Motor Company", Abrv="Ford"},
                new VehicleMakeEntityModel{Name="Automobili Lamborghini S.p.A.", Abrv="Lamborghini"}
            };
            vehicles.ForEach(v => context.VehicleMakes.Add(v));
            context.SaveChanges();

            var vehicleModels = new List<VehicleModelEntityModel>
            {
                new VehicleModelEntityModel{MakeId=1, Name="X5", Abrv="BMW"},
                new VehicleModelEntityModel{MakeId=1, Name="X6", Abrv="BMW"},
                new VehicleModelEntityModel{MakeId=1, Name="M5 Sedan", Abrv="BMW"},
                new VehicleModelEntityModel{MakeId=2, Name="Golf", Abrv="VW"},
                new VehicleModelEntityModel{MakeId=2, Name="Beetle", Abrv="VW"},
                new VehicleModelEntityModel{MakeId=2, Name="Passat", Abrv="VW"},
                new VehicleModelEntityModel{MakeId=3, Name="Mustang", Abrv="Ford"},
                new VehicleModelEntityModel{MakeId=3, Name="Aspire", Abrv="Ford"},
                new VehicleModelEntityModel{MakeId=3, Name="Everest", Abrv="Ford"},
                new VehicleModelEntityModel{MakeId=4, Name="Gallardo", Abrv="Lamborghini"},
                new VehicleModelEntityModel{MakeId=4, Name="Miura", Abrv="Lamborghini"},
                new VehicleModelEntityModel{MakeId=4, Name="Diablo", Abrv="Lamborghini"},
                new VehicleModelEntityModel{MakeId=4, Name="Countach", Abrv="Lamborghini"}
            };
            vehicleModels.ForEach(m => context.VehicleModels.Add(m));
            context.SaveChanges();
        }
    }
}
