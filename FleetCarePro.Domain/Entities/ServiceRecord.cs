using FleetCarePro.Domain.Enums;

namespace FleetCarePro.Domain.Entities;

public class ServiceRecord
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public int ServiceCenterId { get; set; }
    public DateTime ServiceDate { get; set; }
    public int CurrentMileage { get; set; }
    public decimal TotalCost { get; set; }
    public string? InvoiceDocumentPath { get; set; }
    public string? Notes { get; set; }
    public ServiceRecordStatus Status { get; set; }
    public string? CreatedByUserId { get; set; }
    // Navigate
    public Vehicle Vehicle { get; set; }=null!;
    public ServiceCenter ServiceCenter { get; set; }=null!;
    public ApplicationUser? CreatedByUser { get; set; }
    public ICollection<ServiceLineItem> ServiceLineItems { get; set; }=new List<ServiceLineItem>();
}