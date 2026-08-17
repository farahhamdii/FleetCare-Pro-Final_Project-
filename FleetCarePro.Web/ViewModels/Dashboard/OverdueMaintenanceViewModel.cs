namespace FleetCarePro.Web.ViewModels.Dashboard;

public class OverdueMaintenanceViewModel
{
    public int VehicleId { get; set; }

    public string VehicleLicensePlate { get; set; } = null!;

    public DateTime LastServiceDate { get; set; }
}