using FleetCarePro.Application.DTOs.ServiceCenter;

namespace FleetCarePro.Application.Interfaces.Services;

public interface IServiceCenterService
{
    Task<IEnumerable<ServiceCenterDto>> GetAllAsync();
    Task<ServiceCenterDto?> GetByIdAsync(int id);
    Task<ServiceCenterDto> CreateAsync(CreateServiceCenterDto dto);
    Task<bool> UpdateAsync(int id, UpdateServiceCenterDto dto);
    Task<bool> DeleteAsync(int id);
}