using FleetCarePro.Domain.Enums;

namespace FleetCarePro.Application.DTOs.ServiceRecord;

public class CreateServiceRecordDto
{
    public int VehicleId { get; set; }

    public int ServiceCenterId { get; set; }

    public DateTime ServiceDate { get; set; }

    public int CurrentMileage { get; set; }

    public string? InvoiceDocumentPath { get; set; }

    public string? Notes { get; set; }

    public ServiceRecordStatus Status { get; set; }

    public string? CreatedByUserId { get; set; }

    public ICollection<ServiceLineItemDto> ServiceLineItems { get; set; }
        = new List<ServiceLineItemDto>();
}