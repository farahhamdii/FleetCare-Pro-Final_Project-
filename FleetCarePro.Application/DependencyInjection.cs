using AutoMapper;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Application.Mapping;
using FleetCarePro.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FleetCarePro.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IServiceCenterService,ServiceCenterService>();
        services.AddScoped<IServiceCategoryService,ServiceCategoryService>();
        services.AddScoped<IServiceRecordService,ServiceRecordService>();
        return services;
    }
}