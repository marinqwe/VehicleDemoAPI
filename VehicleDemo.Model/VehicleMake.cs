﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VehicleDemo.Model.Common;

namespace VehicleDemo.Model
{
    public class VehicleMake : IVehicleMake
    {
        public int MakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
