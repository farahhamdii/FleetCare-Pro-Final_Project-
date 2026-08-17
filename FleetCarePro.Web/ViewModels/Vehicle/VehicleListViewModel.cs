using FleetCarePro.Domain.Enums;

namespace FleetCarePro.Web.ViewModels.Vehicle;

public class VehicleListViewModel
{
    public int Id { get; set; }

    public string VIN { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public VehicleStatus Status { get; set; }

    public string? VehicleImageURL { get; set; }

    public string? DriverId { get; set; }
}