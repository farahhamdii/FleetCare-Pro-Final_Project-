using FleetCarePro.Domain.Enums;
using FleetCarePro.Web.Validation;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.Vehicle;

public class EditVehicleViewModel
{
    public int Id { get; set; }

    [Required]
    [ValidVIN]
    public string VIN { get; set; } = null!;

    [Required]
    public string LicensePlate { get; set; } = null!;

    [Required]
    public string Make { get; set; } = null!;

    [Required]
    public string Model { get; set; } = null!;

    [Range(1900, 2100)]
    public int Year { get; set; }

    [Range(0, double.MaxValue)]
    public decimal PurchasePrice { get; set; }

    public VehicleStatus Status { get; set; }

    [Range(0, int.MaxValue)]
    public int Mileage { get; set; }

    public string? DriverId { get; set; }

    [VehicleImageValidation]
    public IFormFile? VehicleImage { get; set; }

    public string? ExistingImageUrl { get; set; }
}