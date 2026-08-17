using FleetCarePro.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.ServiceRecord;

public class EditServiceRecordViewModel
{
    public int Id { get; set; }

    [Required]
    public int VehicleId { get; set; }

    [Required]
    public int ServiceCenterId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ServiceDate { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int CurrentMileage { get; set; }

    public string? InvoiceDocumentPath { get; set; }

    public IFormFile? InvoiceFile { get; set; }

    public string? Notes { get; set; }

    [Required]
    public ServiceRecordStatus Status { get; set; }

    public List<ServiceLineItemViewModel> ServiceLineItems { get; set; }
        = new List<ServiceLineItemViewModel>();
}