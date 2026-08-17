
namespace FleetCarePro.Web.ViewModels.AuditLog;

public class AuditLogViewModel
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string Action { get; set; } = null!;

    public string? Details { get; set; }

    public DateTime Timestamp { get; set; }
}