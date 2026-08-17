using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceRecord;
using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Services;

public class ServiceRecordService : IServiceRecordService
{
    private readonly IServiceRecordRepository _repository;
    private readonly IMapper _mapper;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IServiceCenterRepository _serviceCenterRepository;

    public ServiceRecordService(
        IServiceRecordRepository repository,
        IVehicleRepository vehicleRepository,
        IServiceCenterRepository serviceCenterRepository,
        IMapper mapper)
    {
        _repository = repository;
        _vehicleRepository = vehicleRepository;
        _serviceCenterRepository = serviceCenterRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ServiceRecordDto>> GetAllAsync()
    {
        var records = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ServiceRecordDto>>(records);
    }

    public async Task<ServiceRecordDto?> GetByIdAsync(int id)
    {
        var record = await _repository.GetByIdAsync(id);
        if (record == null)
            return null;
        return _mapper.Map<ServiceRecordDto>(record);
    }

    public async Task<ServiceRecordDto> CreateAsync(CreateServiceRecordDto dto)
    {
        var vehicle =await _vehicleRepository.GetByIdAsync(dto.VehicleId);
        if (vehicle == null)throw new ArgumentException("Vehicle not found.");
        var serviceCenter =await _serviceCenterRepository.GetByIdAsync(dto.ServiceCenterId);
        if (serviceCenter == null)
            throw new ArgumentException("Service center not found.");

        var serviceRecord = _mapper.Map<ServiceRecord>(dto);
        serviceRecord.TotalCost = serviceRecord.ServiceLineItems.Sum(item => item.Cost);
        await _repository.CreateWithTransactionAsync(serviceRecord);
        return _mapper.Map<ServiceRecordDto>(serviceRecord);
    }

    public async Task<bool> UpdateAsync(int id,UpdateServiceRecordDto dto)
    {
        var record = await _repository.GetByIdAsync(id);
        if (record == null)
            return false;
        var vehicle = await _vehicleRepository.GetByIdAsync(dto.VehicleId);
        if (vehicle == null)
            throw new ArgumentException("Vehicle not found.");

        var serviceCenter = await _serviceCenterRepository.GetByIdAsync(dto.ServiceCenterId);

        if (serviceCenter == null)
            throw new ArgumentException("Service center not found.");

        // Update
        record.VehicleId = dto.VehicleId;
        record.ServiceCenterId = dto.ServiceCenterId;
        record.ServiceDate = dto.ServiceDate;
        record.CurrentMileage = dto.CurrentMileage;
        record.InvoiceDocumentPath = dto.InvoiceDocumentPath;
        record.Notes = dto.Notes;
        record.Status = dto.Status;


        record.ServiceLineItems.Clear();


        foreach (var itemDto in dto.ServiceLineItems)
        {
            record.ServiceLineItems.Add(
                new ServiceLineItem
                {
                    ServiceCategoryId =itemDto.ServiceCategoryId,
                    Description = itemDto.Description,
                    Cost =itemDto.Cost
                });
        }

        //total
        record.TotalCost =record.ServiceLineItems.Sum(item => item.Cost);
        _repository.Update(record);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var record =await _repository.GetByIdAsync(id);
        if (record == null)
            return false;
        _repository.Delete(record);
        await _repository.SaveChangesAsync();
        return true;
    }
}