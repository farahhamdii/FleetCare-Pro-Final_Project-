using FleetCarePro.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.ServiceRecord;

public class CreateServiceRecordViewModel
{
    [Required]
    public int VehicleId { get; set; }

    [Required]
    public int ServiceCenterId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ServiceDate { get; set; } = DateTime.Today;

    [Required]
    [Range(0, int.MaxValue)]
    public int CurrentMileage { get; set; }

    public IFormFile? InvoiceFile { get; set; }

    public string? Notes { get; set; }

    [Required]
    public ServiceRecordStatus Status { get; set; }

    public ICollection<ServiceLineItemViewModel> ServiceLineItems { get; set; }
        = new List<ServiceLineItemViewModel>();
}