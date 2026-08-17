using FleetCarePro.Application.DTOs.Vehicle;

namespace FleetCarePro.Application.Interfaces.Services;

public interface IVehicleService
{
    Task<IEnumerable<VehicleDto>> GetAllAsync();
    Task<IEnumerable<VehicleDto>> GetAssignedVehiclesAsync(string driverId);
    Task<VehicleDto?> GetByIdAsync(int id);
    Task<VehicleDto?> GetByIdForDriverAsync(int id, string driverId);
    Task<VehicleDto> CreateAsync(CreateVehicleDto dto);
    Task<bool> UpdateAsync(int id, UpdateVehicleDto dto);
    Task<bool> DeleteAsync(int id);
}