using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using VehicleDemo.Model;
using VehicleDemo.Model.Common;

namespace VehicleDemo.DAL
{
    public class VehicleContext : DbContext
    {
        public VehicleContext()
       : base("VehicleContext")
        {
            // Write queries to debug output window
            Database.Log = sql => Debug.WriteLine(sql);
        }

        public DbSet<VehicleMakeEntityModel> VehicleMakes { get; set; }
        public DbSet<VehicleModelEntityModel> VehicleModels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}