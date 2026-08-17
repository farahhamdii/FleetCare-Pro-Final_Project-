using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Interfaces.Infrastructure;

public interface IServiceRecordRepository
{
    Task<IEnumerable<ServiceRecord>> GetAllAsync();

    Task<ServiceRecord?> GetByIdAsync(int id);

    Task AddAsync(ServiceRecord serviceRecord);

    void Update(ServiceRecord serviceRecord);

    void Delete(ServiceRecord serviceRecord);

    Task SaveChangesAsync();

    Task CreateWithTransactionAsync(ServiceRecord serviceRecord);
}