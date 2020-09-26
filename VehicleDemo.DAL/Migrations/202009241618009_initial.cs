namespace VehicleDemo.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VehicleMakeEntityModel",
                c => new
                    {
                        MakeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Abrv = c.String(),
                    })
                .PrimaryKey(t => t.MakeId);
            
            CreateTable(
                "dbo.VehicleModelEntityModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MakeId = c.Int(nullable: false),
                        Name = c.String(),
                        Abrv = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.VehicleModelEntityModel");
            DropTable("dbo.VehicleMakeEntityModel");
        }
    }
}
