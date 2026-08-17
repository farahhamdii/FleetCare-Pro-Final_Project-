
using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FleetCarePro.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly AppDbContext _context;
    public VehicleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vehicle>> GetAllAsync()
    {
        return await _context.Vehicles.Include(v=>v.Driver)
            .ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _context.Vehicles .Include(v=>v.Driver)
            .FirstOrDefaultAsync(v=>v.Id==id);
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        await _context.Vehicles.AddAsync(vehicle);
    }

    public void Update(Vehicle vehicle)
    {
        _context.Vehicles.Update(vehicle);
    }

    public void Delete(Vehicle vehicle)
    {
        _context.Vehicles.Remove(vehicle);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Vehicles.AnyAsync(v => v.Id == id);
    }

    public async Task<IEnumerable<Vehicle>> GetAssignedVehiclesAsync(string driverId)
    {
        return await _context.Vehicles .Include(v => v.Driver)
            .Where(v => v.DriverId == driverId).ToListAsync();
    }

    public async Task<Vehicle?> GetByIdForDriverAsync( int id,string driverId)
    {
        return await _context.Vehicles.Include(v=>v.Driver)
            .FirstOrDefaultAsync(v=>v.Id==id&&v.DriverId==driverId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

