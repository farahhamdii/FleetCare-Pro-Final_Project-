namespace FleetCarePro.Web.ViewModels.Dashboard;

public class DashboardViewModel
{
    public string WelcomeMessage { get; set; } = null!;

    public int PendingServices { get; set; }

    public int ApprovedServices { get; set; }

    public int CompletedServices { get; set; }

    public int CancelledServices { get; set; }
    public int TotalServiceCategories { get; set; }
    public int TotalVehicles { get; set; }
    public int TotalServiceCenters { get; set; }
    public int TotalServiceRecords { get; set; }

    // Admin 
    public int TotalUsers { get; set; }

    //  Manager
    // Driver
    public int AssignedVehicles { get; set; }
}