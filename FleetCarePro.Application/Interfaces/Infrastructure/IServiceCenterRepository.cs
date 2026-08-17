using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Interfaces.Infrastructure;

public interface IServiceCenterRepository
{
    Task<IEnumerable<ServiceCenter>> GetAllAsync();

    Task<ServiceCenter?> GetByIdAsync(int id);

    Task AddAsync(ServiceCenter serviceCenter);

    void Update(ServiceCenter serviceCenter);

    void Delete(ServiceCenter serviceCenter);

    Task SaveChangesAsync();
}