namespace FleetCarePro.Domain.Entities;

public class VendorService
{
    public int ServiceCenterId { get; set; }
    public int ServiceCategoryId { get; set; }
    public ServiceCenter ServiceCenter { get; set; } = null!;
    public ServiceCategory ServiceCategory { get; set; } = null!;
}