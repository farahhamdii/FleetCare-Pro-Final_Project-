using FleetCarePro.Domain.Enums;

namespace FleetCarePro.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string VIN { get; set; }=null!;
    public string LicensePlate { get; set; }=null!;
    public string Make { get; set; }=null!;
    public string Model { get; set; }=null!;
    public int Year { get; set; }
    public decimal PurchasePrice { get; set; }
    public VehicleStatus Status { get; set; }
    public int Mileage { get; set; }
    public string? VehicleImageURL { get; set; }
    //fk
    public string? DriverId { get; set; }

    // Navigate
    public ApplicationUser? Driver { get; set; }
    public ICollection<ServiceRecord> ServiceRecords { get; set; }=new List<ServiceRecord>();
}