using System.ComponentModel.DataAnnotations;
using VehicleDemo.Model.Common;

namespace VehicleDemo.DAL
{
    public class VehicleModelEntityModel
    {
        [Key]
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public IVehicleMake VehicleMake { get; set; }
    }
}
