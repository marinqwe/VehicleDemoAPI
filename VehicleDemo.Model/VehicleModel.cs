using VehicleDemo.Model.Common;

namespace VehicleDemo.Model
{
    public class VehicleModel : IVehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
        public IVehicleMake VehicleMake { get; set; }
    }
}
