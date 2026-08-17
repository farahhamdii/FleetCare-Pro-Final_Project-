namespace FleetCarePro.Domain.Entities;

public class ServiceCenter
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public bool IsActive { get; set; }
    public ICollection<VendorService> VendorServices { get; set; }=new List<VendorService>();
    public ICollection<ServiceRecord> ServiceRecords { get; set; }=new List<ServiceRecord>();
}