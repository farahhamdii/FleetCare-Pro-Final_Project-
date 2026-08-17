using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceCenter;
using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Services;

public class ServiceCenterService : IServiceCenterService
{
    private readonly IServiceCenterRepository _repository;
    private readonly IMapper _mapper;

    public ServiceCenterService(IServiceCenterRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ServiceCenterDto>> GetAllAsync()
    {
        var serviceCenters=await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ServiceCenterDto>>(serviceCenters);
    }

    public async Task<ServiceCenterDto?> GetByIdAsync(int id)
    {
        var serviceCenter=await _repository.GetByIdAsync(id);
        if (serviceCenter == null) return null;
        return _mapper.Map<ServiceCenterDto>(serviceCenter);
    }

    public async Task<ServiceCenterDto> CreateAsync( CreateServiceCenterDto dto)
    {
        var serviceCenter= _mapper.Map<ServiceCenter>(dto);
        serviceCenter.VendorServices.Clear();
        foreach (var categoryId in dto.SelectedServiceCategoryIds.Distinct())
        {
            serviceCenter.VendorServices.Add(
                new VendorService
                {
                    ServiceCategoryId = categoryId
                });
        }

        await _repository.AddAsync(serviceCenter);
        await _repository.SaveChangesAsync();
        return _mapper.Map<ServiceCenterDto>(serviceCenter);
    }

    public async Task<bool> UpdateAsync(int id,UpdateServiceCenterDto dto)
    {
        var serviceCenter =await _repository.GetByIdAsync(id);
        if (serviceCenter == null)
            return false;

        serviceCenter.Name =dto.Name;
        serviceCenter.PhoneNumber =dto.PhoneNumber;
        serviceCenter.Email =dto.Email;
        serviceCenter.Address =dto.Address;
        serviceCenter.IsActive =dto.IsActive;

        serviceCenter.VendorServices.Clear();

        foreach (var categoryId in dto.SelectedServiceCategoryIds.Distinct())
        {
            serviceCenter.VendorServices.Add(
                new VendorService
                {
                    ServiceCenterId = serviceCenter.Id,
                    ServiceCategoryId = categoryId
                });
        }

        _repository.Update(serviceCenter);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var serviceCenter =await _repository.GetByIdAsync(id);
        if (serviceCenter == null)
            return false;
        _repository.Delete(serviceCenter);
        await _repository.SaveChangesAsync();
        return true;
    }
}