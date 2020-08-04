using System;
using VehicleDemo.Common.IHelpers;

namespace VehicleDemo.Common.Helpers
{
    public class VehicleSorting : IVehicleSorting
    {
        public string SortBy { get; set; }
        public string SortByName { get; set; }
        public string SortByAbrv { get; set; }
        public string SortById { get; set; }

        public VehicleSorting(string sortBy)
        {
            SortBy = sortBy;
            SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            SortByAbrv = sortBy == "abrv" ? "abrv_desc" : "abrv";
            SortById = sortBy == "makeId" ? "makeId_desc" : "makeId";
        }
    }
}
