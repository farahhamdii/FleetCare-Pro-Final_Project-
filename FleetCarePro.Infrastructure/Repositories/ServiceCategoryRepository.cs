using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetCarePro.Infrastructure.Repositories;

public class ServiceCategoryRepository : IServiceCategoryRepository
{
    private readonly AppDbContext _context;

    public ServiceCategoryRepository(AppDbContext context)
    {
        _context=context;
    }

    public async Task<IEnumerable<ServiceCategory>> GetAllAsync()
    {
        return await _context.ServiceCategories.ToListAsync();
    }

    public async Task<ServiceCategory?> GetByIdAsync(int id)
    {
        return await _context.ServiceCategories
            .FirstOrDefaultAsync(c =>c.Id == id);
    }

    public async Task AddAsync(ServiceCategory category)
    {
        await _context.ServiceCategories.AddAsync(category);
    }

    public void Update(ServiceCategory category)
    {
        _context.ServiceCategories.Update(category);
    }

    public void Delete(ServiceCategory category)
    {
        _context.ServiceCategories.Remove(category);
    }
    public async Task<bool> HasServiceLineItemsAsync(int categoryId)
    {
        return await _context.ServiceLineItems
            .AnyAsync(x => x.ServiceCategoryId == categoryId);
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}