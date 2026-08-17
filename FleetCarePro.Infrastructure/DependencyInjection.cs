using FleetCarePro.Application.Interfaces.Infrastructure;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Application.Mapping;
using FleetCarePro.Infrastructure.Repositories;
using FleetCarePro.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FleetCarePro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(MappingProfile));
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IServiceCenterRepository,ServiceCenterRepository>();
        services.AddScoped<IServiceCategoryRepository,ServiceCategoryRepository>();
        services.AddScoped<IServiceRecordRepository,ServiceRecordRepository>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAuditLogService, AuditLogService>();

        return services;

    }
}