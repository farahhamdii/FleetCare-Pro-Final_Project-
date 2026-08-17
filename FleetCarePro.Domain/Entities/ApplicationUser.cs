using Microsoft.AspNetCore.Identity;

namespace FleetCarePro.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = null!;
    public string EmployeeId { get; set; } = null!;
    public ICollection<Vehicle> AssignedVehicles { get; set; }= new List<Vehicle>();
    public ICollection<ServiceRecord> CreatedServiceRecords { get; set; }= new List<ServiceRecord>();
    public ICollection<AuditLog> AuditLogs { get; set; } =new List<AuditLog>();
}