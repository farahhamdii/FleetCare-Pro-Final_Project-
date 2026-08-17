
using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Interfaces.Services;

public interface IAuditLogService
{
    Task LogAsync( string action,string? details = null,string? userId = null);
    Task<IEnumerable<AuditLog>> GetAllAsync();
}
