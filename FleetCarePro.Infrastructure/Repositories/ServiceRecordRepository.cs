using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetCarePro.Infrastructure.Repositories;

public class ServiceRecordRepository : IServiceRecordRepository
{
    private readonly AppDbContext _context;
    public ServiceRecordRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<IEnumerable<ServiceRecord>> GetAllAsync()
    {
        return await _context.ServiceRecords.Include(sr => sr.Vehicle)
            .Include(sr => sr.ServiceCenter)
            .Include(sr => sr.ServiceLineItems).ThenInclude(li => li.ServiceCategory)
            .ToListAsync();
    }

    public async Task<ServiceRecord?> GetByIdAsync(int id)
    {
        return await _context.ServiceRecords.Include(sr => sr.Vehicle)
            .Include(sr => sr.ServiceCenter)
            .Include(sr => sr.ServiceLineItems).ThenInclude(li => li.ServiceCategory)
            .FirstOrDefaultAsync(sr => sr.Id == id);
    }

    public async Task AddAsync(ServiceRecord serviceRecord)
    {
        await _context.ServiceRecords.AddAsync(serviceRecord);
    }

    public void Update(ServiceRecord serviceRecord)
    {
        _context.ServiceRecords.Update(serviceRecord);
    }

    public void Delete(ServiceRecord serviceRecord)
    {
        _context.ServiceRecords.Remove(serviceRecord);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task CreateWithTransactionAsync(
        ServiceRecord serviceRecord)
    {
        await using var transaction =await _context.Database.BeginTransactionAsync();

        try
        {
            await _context.ServiceRecords.AddAsync(serviceRecord);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}