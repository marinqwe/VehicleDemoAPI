using VehicleDemo.Common.IHelpers;

namespace VehicleDemo.Common.Helpers
{
    public class VehicleFilters : IVehicleFilters
    {
        public string SearchString { get; set; }
        public string FilterBy { get; set; }

        public VehicleFilters(string searchString)
        {
            SearchString = searchString;
        }

        public bool ShouldApplyFilters()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                FilterBy = SearchString;
                return true;
            }
            return false;
        }
    }
}
