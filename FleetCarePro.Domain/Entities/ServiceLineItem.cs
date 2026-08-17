namespace FleetCarePro.Domain.Entities;

public class ServiceLineItem
{
    public int Id { get; set; }
    public int ServiceRecordId { get; set; }
    public int ServiceCategoryId { get; set; }
    public string Description { get; set; } = null!;
    public decimal Cost { get; set; }
    // Navigate
    public ServiceRecord ServiceRecord { get; set; } = null!;
    public ServiceCategory ServiceCategory { get; set; } = null!;
}