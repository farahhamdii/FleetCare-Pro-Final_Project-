using FleetCarePro.Domain.Enums;

namespace FleetCarePro.Web.ViewModels.ServiceRecord;

public class ServiceRecordListViewModel
{
    public int Id { get; set; }

    public string? VehicleLicensePlate { get; set; }

    public string? ServiceCenterName { get; set; }

    public DateTime ServiceDate { get; set; }

    public int CurrentMileage { get; set; }

    public decimal TotalCost { get; set; }

    public ServiceRecordStatus Status { get; set; }
}