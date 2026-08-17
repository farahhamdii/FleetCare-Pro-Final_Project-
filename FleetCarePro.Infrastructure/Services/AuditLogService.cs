
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetCarePro.Infrastructure.Services;

public class AuditLogService : IAuditLogService
{
    private readonly AppDbContext _context;
    public AuditLogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task LogAsync(string action, string? details = null, string? userId = null)
    {
        var auditLog = new AuditLog
        {
            Action=action,
            Details=details,
            UserId=userId,
            Timestamp = DateTime.UtcNow
        };

        await _context.AuditLogs.AddAsync(auditLog);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AuditLog>> GetAllAsync()
    {
        return await _context.AuditLogs.Include(x => x.User)
            .OrderByDescending(x => x.Timestamp).ToListAsync();
    }
}
