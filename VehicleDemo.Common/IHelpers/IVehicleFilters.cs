namespace VehicleDemo.Common.IHelpers
{
    public interface IVehicleFilters
    {
        string SearchString { get; set; }
        string FilterBy { get; set; }
        bool ShouldApplyFilters();
    }
}
