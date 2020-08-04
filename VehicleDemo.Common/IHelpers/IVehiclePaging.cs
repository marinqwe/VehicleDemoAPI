using System;
using System.Collections.Generic;
namespace VehicleDemo.Common.IHelpers
{
    public interface IVehiclePaging
    {
        int? Page { get; set; }
        int ItemsToSkip { get; set; }
        int TotalCount { get; set; }
        int ResultsPerPage { get; set; }
    }
}
