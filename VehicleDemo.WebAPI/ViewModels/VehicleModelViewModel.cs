using System.ComponentModel.DataAnnotations;

namespace VehicleDemo.WebAPI.ViewModels
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}