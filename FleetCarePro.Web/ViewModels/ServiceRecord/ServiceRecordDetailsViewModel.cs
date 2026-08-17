using FleetCarePro.Domain.Enums;

namespace FleetCarePro.Web.ViewModels.ServiceRecord;

public class ServiceRecordDetailsViewModel
{
    public int Id { get; set; }

    public string? VehicleLicensePlate { get; set; }

    public string? ServiceCenterName { get; set; }

    public DateTime ServiceDate { get; set; }

    public int CurrentMileage { get; set; }

    public decimal TotalCost { get; set; }


    public string? InvoiceDocumentPath { get; set; }

    public IFormFile? InvoiceFile { get; set; }


    public string? Notes { get; set; }

    public ServiceRecordStatus Status { get; set; }

    public ICollection<ServiceLineItemDetailsViewModel> ServiceLineItems { get; set; }
        = new List<ServiceLineItemDetailsViewModel>();
}