using FleetCarePro.Application.DTOs.ServiceRecord;

namespace FleetCarePro.Application.Interfaces.Services;

public interface IServiceRecordService
{
    Task<IEnumerable<ServiceRecordDto>> GetAllAsync();
    Task<ServiceRecordDto?> GetByIdAsync(int id);
    Task<ServiceRecordDto> CreateAsync(
        CreateServiceRecordDto dto);
    Task<bool> UpdateAsync( int id,UpdateServiceRecordDto dto);
    Task<bool> DeleteAsync(int id);
}