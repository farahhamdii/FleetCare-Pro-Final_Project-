namespace FleetCarePro.Domain.Entities;

public class ServiceCategory
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public int RecommendedIntervalMonths { get; set; }
    public ICollection<VendorService> VendorServices { get; set; }= new List<VendorService>();
    public ICollection<ServiceLineItem> ServiceLineItems { get; set; }= new List<ServiceLineItem>();
}