using AutoMapper;
using FleetCarePro.Application.DTOs.Vehicle;
using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IServiceRecordRepository _serviceRecordRepository;
    private readonly IMapper _mapper;

    public VehicleService(
        IVehicleRepository vehicleRepository,
        IMapper mapper,IServiceRecordRepository serviceRecordRepository)
    {
        _vehicleRepository = vehicleRepository;
        _mapper = mapper;
        _serviceRecordRepository = serviceRecordRepository;
    }

    public async Task<IEnumerable<VehicleDto>> GetAllAsync()
    {
        var vehicles = await _vehicleRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
    }

    public async Task<VehicleDto?> GetByIdAsync(int id)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null)
            return null;
        return _mapper.Map<VehicleDto>(vehicle);
    }

    public async Task<VehicleDto> CreateAsync(CreateVehicleDto dto)
    {
        var vehicle = _mapper.Map<Vehicle>(dto);
        await _vehicleRepository.AddAsync(vehicle);
        await _vehicleRepository.SaveChangesAsync();
        return _mapper.Map<VehicleDto>(vehicle);
    }

    public async Task<bool> UpdateAsync( int id,UpdateVehicleDto dto)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(id);
        if (vehicle == null)
            return false;
        _mapper.Map(dto, vehicle);
        _vehicleRepository.Update(vehicle);
        await _vehicleRepository.SaveChangesAsync();

        return true;
    }
    public async Task<IEnumerable<VehicleDto>> GetAssignedVehiclesAsync(
    string driverId)
    {
        var vehicles =await _vehicleRepository.GetAssignedVehiclesAsync(driverId);
        return _mapper.Map<IEnumerable<VehicleDto>>(vehicles);
    }

    public async Task<VehicleDto?> GetByIdForDriverAsync(int id,string driverId)
    {
        var vehicle =await _vehicleRepository.GetByIdForDriverAsync(id, driverId);
        if (vehicle == null)
            return null;
        return _mapper.Map<VehicleDto>(vehicle);
    }
    public async Task<bool> DeleteAsync(int vehicleId)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);
        if (vehicle == null)
            return false;
        var serviceRecords = await _serviceRecordRepository.GetAllAsync();
        var hasServiceRecords = serviceRecords
            .Any(x => x.VehicleId == vehicleId);
        if (hasServiceRecords)
            return false;
         _vehicleRepository.Delete(vehicle);
        return true;
    }
}