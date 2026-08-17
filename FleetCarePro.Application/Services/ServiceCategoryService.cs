using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceCategory;
using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Services;

public class ServiceCategoryService : IServiceCategoryService
{
    private readonly IServiceCategoryRepository _repository;
    private readonly IMapper _mapper;

    public ServiceCategoryService( IServiceCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ServiceCategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ServiceCategoryDto>>(categories);
    }

    public async Task<ServiceCategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            return null;
        return _mapper.Map<ServiceCategoryDto>(category);
    }

    public async Task<ServiceCategoryDto> CreateAsync(CreateServiceCategoryDto dto)
    {
        var category = _mapper.Map<ServiceCategory>(dto);
        await _repository.AddAsync(category);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ServiceCategoryDto>(category);
    }

    public async Task<bool> UpdateAsync(int id,UpdateServiceCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            return false;
        _mapper.Map(dto, category);
        _repository.Update(category);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)return false;
        _repository.Delete(category);
        await _repository.SaveChangesAsync();
        return true;
    }
}