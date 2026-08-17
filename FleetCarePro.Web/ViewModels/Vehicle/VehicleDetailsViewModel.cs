using FleetCarePro.Domain.Enums;

namespace FleetCarePro.Web.ViewModels.Vehicle;

public class VehicleDetailsViewModel
{
    public int Id { get; set; }

    public string VIN { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public decimal PurchasePrice { get; set; }

    public VehicleStatus Status { get; set; }

    public int Mileage { get; set; }

    public string? VehicleImageURL { get; set; }

    public string? DriverId { get; set; }
    public string? DriverName { get; set; }
}