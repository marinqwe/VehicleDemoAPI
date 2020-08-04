using System;
using System.Collections.Generic;
namespace VehicleDemo.Common.IHelpers
{
    public interface IVehicleSorting
    {
        string SortBy { get; set; }
        string SortByName { get; set; }
        string SortByAbrv { get; set; }
        string SortById { get; set; }
    }
}
