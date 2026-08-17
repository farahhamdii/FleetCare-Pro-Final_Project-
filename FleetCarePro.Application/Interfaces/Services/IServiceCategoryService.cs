using FleetCarePro.Application.DTOs.ServiceCategory;

namespace FleetCarePro.Application.Interfaces.Services;

public interface IServiceCategoryService
{
    Task<IEnumerable<ServiceCategoryDto>> GetAllAsync();

    Task<ServiceCategoryDto?> GetByIdAsync(int id);

    Task<ServiceCategoryDto> CreateAsync(CreateServiceCategoryDto dto);

    Task<bool> UpdateAsync(int id, UpdateServiceCategoryDto dto);

    Task<bool> DeleteAsync(int id);
}