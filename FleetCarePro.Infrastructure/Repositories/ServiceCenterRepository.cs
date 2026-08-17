using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetCarePro.Infrastructure.Repositories;

public class ServiceCenterRepository : IServiceCenterRepository
{
    private readonly AppDbContext _context;
    public ServiceCenterRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<IEnumerable<ServiceCenter>> GetAllAsync()
    {
        return await _context.ServiceCenters.Include(sc => sc.VendorServices)
                .ThenInclude(vs => vs.ServiceCategory).ToListAsync();
    }

    public async Task<ServiceCenter?> GetByIdAsync(int id)
    {
        return await _context.ServiceCenters.Include(sc=>sc.VendorServices)
                .ThenInclude(vs =>vs.ServiceCategory).FirstOrDefaultAsync(sc => sc.Id == id);
    }

    public async Task AddAsync(ServiceCenter serviceCenter)
    {
        await _context.ServiceCenters.AddAsync(serviceCenter);
    }

    public void Update(ServiceCenter serviceCenter)
    {
        _context.ServiceCenters.Update(serviceCenter);
    }

    public void Delete(ServiceCenter serviceCenter)
    {
        _context.ServiceCenters.Remove(serviceCenter);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}