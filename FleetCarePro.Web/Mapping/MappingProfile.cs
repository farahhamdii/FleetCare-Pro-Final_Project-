using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceCategory;
using FleetCarePro.Application.DTOs.ServiceCenter;
using FleetCarePro.Application.DTOs.ServiceRecord;
using FleetCarePro.Application.DTOs.Vehicle;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Web.ViewModels.AuditLog;
using FleetCarePro.Web.ViewModels.ServiceCategory;
using FleetCarePro.Web.ViewModels.ServiceCenter;
using FleetCarePro.Web.ViewModels.ServiceRecord;
using FleetCarePro.Web.ViewModels.Vehicle;
using System;

namespace FleetCarePro.Web.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VehicleDto, VehicleListViewModel>();
        CreateMap<VehicleDto, VehicleDetailsViewModel>();
        CreateMap<VehicleDto, EditVehicleViewModel>();

        CreateMap<CreateVehicleViewModel, CreateVehicleDto>();
        CreateMap<EditVehicleViewModel, UpdateVehicleDto>();

        CreateMap<ServiceCenterDto, ServiceCenterListViewModel>();
        CreateMap<ServiceCenterDto, ServiceCenterDetailsViewModel>();
        CreateMap<ServiceCenterDto, EditServiceCenterViewModel>();
        CreateMap<CreateServiceCenterViewModel, CreateServiceCenterDto>(); 
        CreateMap<EditServiceCenterViewModel, UpdateServiceCenterDto>();

        CreateMap<ServiceCategoryDto, ServiceCategoryListViewModel>();
        CreateMap<ServiceCategoryDto, ServiceCategoryDetailsViewModel>();
        CreateMap<ServiceCategoryDto, EditServiceCategoryViewModel>();

        CreateMap<CreateServiceCategoryViewModel, CreateServiceCategoryDto>();
        CreateMap<EditServiceCategoryViewModel, UpdateServiceCategoryDto>();

        CreateMap<ServiceRecordDto, ServiceRecordListViewModel>();

        CreateMap<ServiceRecordDto, ServiceRecordDetailsViewModel>();

        CreateMap<ServiceRecordDto, EditServiceRecordViewModel>();

        CreateMap<ServiceLineItemDto, ServiceLineItemDetailsViewModel>();

        CreateMap<CreateServiceRecordViewModel, CreateServiceRecordDto>();

        CreateMap<EditServiceRecordViewModel, UpdateServiceRecordDto>();

        CreateMap<ServiceLineItemViewModel, ServiceLineItemDto>();
        CreateMap<ServiceLineItemDto, ServiceLineItemViewModel>();

        CreateMap<ServiceRecordDto, EditServiceRecordViewModel>();
        CreateMap<EditServiceRecordViewModel, UpdateServiceRecordDto>();

        CreateMap<ServiceLineItemDto, ServiceLineItemViewModel>();
        CreateMap<ServiceLineItemViewModel, ServiceLineItemDto>();
      
CreateMap<AuditLog, AuditLogViewModel>()
    .ForMember(
        dest => dest.UserName,
        opt => opt.MapFrom(src => src.User != null
            ? src.User.FullName
            : "System"))
    .ForMember(
        dest => dest.UserEmail,
        opt => opt.MapFrom(src => src.User != null
            ? src.User.Email
            : "-"));


    }
}