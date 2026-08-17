using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Interfaces.Infrastructure;

public interface IVehicleRepository
{
    Task<IEnumerable<Vehicle>> GetAllAsync();

    Task<Vehicle?> GetByIdAsync(int id);

    Task<IEnumerable<Vehicle>> GetAssignedVehiclesAsync(string driverId);

    Task<Vehicle?> GetByIdForDriverAsync(int id, string driverId);

    Task AddAsync(Vehicle vehicle);

    void Update(Vehicle vehicle);

    void Delete(Vehicle vehicle);

    Task SaveChangesAsync();
}