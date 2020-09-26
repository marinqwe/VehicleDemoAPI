using System;
using System.Collections.Generic;
using System.Text;
using VehicleDemo.Model;

namespace VehicleDemo.Model.Common
{
    public interface IVehicleMake
    {
        int MakeId { get; set; }
        string Name { get; set; }
        string Abrv { get; set; }
    }
}
