namespace VehicleDemo.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedmodellocations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VehicleModel", "MakeId", "dbo.VehicleMake");
            DropIndex("dbo.VehicleModel", new[] { "MakeId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.VehicleModel", "MakeId");
            AddForeignKey("dbo.VehicleModel", "MakeId", "dbo.VehicleMake", "MakeId", cascadeDelete: true);
        }
    }
}
