using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Interfaces.Infrastructure;

public interface IServiceCategoryRepository
{
    Task<IEnumerable<ServiceCategory>> GetAllAsync();

    Task<ServiceCategory?> GetByIdAsync(int id);

    Task AddAsync(ServiceCategory category);

    void Update(ServiceCategory category);

    void Delete(ServiceCategory category);

    Task<bool> HasServiceLineItemsAsync(int categoryId);

    Task SaveChangesAsync();
}