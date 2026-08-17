using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceCategory;
using FleetCarePro.Application.DTOs.ServiceCenter;
using FleetCarePro.Application.DTOs.ServiceRecord;
using FleetCarePro.Application.DTOs.Vehicle;
using FleetCarePro.Domain.Entities;

namespace FleetCarePro.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vehicle, VehicleDto>()
            .ForMember(dest=>dest.DriverName,opt=>opt.MapFrom(src =>src.Driver != null? src.Driver.FullName: null));
        CreateMap<CreateVehicleDto, Vehicle>();
        CreateMap<UpdateVehicleDto, Vehicle>();



        CreateMap<ServiceCenter, ServiceCenterDto>()
        .ForMember(dest=>dest.SelectedServiceCategoryIds,opt=>opt.MapFrom(src => src.VendorServices.Select(vs => vs.ServiceCategoryId).ToList()))
        .ForMember(dest=>dest.ServiceCategories,opt=>opt.MapFrom(src =>
                src.VendorServices
                    .Where(vs=>vs.ServiceCategory != null)
                    .Select(vs=>vs.ServiceCategory.CategoryName)
                    .ToList()));

        CreateMap<CreateServiceCenterDto, ServiceCenter>();
        CreateMap<UpdateServiceCenterDto, ServiceCenter>();



        CreateMap<ServiceCategory, ServiceCategoryDto>();
        CreateMap<CreateServiceCategoryDto, ServiceCategory>();
        CreateMap<UpdateServiceCategoryDto, ServiceCategory>();



        CreateMap<ServiceLineItem, ServiceLineItemDto>()
            .ForMember(dest=>dest.CategoryName,opt=>opt.MapFrom(src=>src.ServiceCategory.CategoryName));
        CreateMap<ServiceLineItemDto, ServiceLineItem>();



        CreateMap<ServiceRecord, ServiceRecordDto>()
            .ForMember(dest => dest.VehicleLicensePlate,opt => opt.MapFrom(src =>src.Vehicle.LicensePlate))
            .ForMember( dest => dest.ServiceCenterName,opt => opt.MapFrom(src =>src.ServiceCenter.Name));
        CreateMap<CreateServiceRecordDto, ServiceRecord>();
        CreateMap<UpdateServiceRecordDto, ServiceRecord>();
    }
}






