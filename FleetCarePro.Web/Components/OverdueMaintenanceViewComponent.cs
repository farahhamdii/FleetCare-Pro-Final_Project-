using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;

namespace FleetCarePro.Web.Components;

public class OverdueMaintenanceViewComponent : ViewComponent
{
    private readonly IServiceRecordService _serviceRecordService;

    public OverdueMaintenanceViewComponent(
        IServiceRecordService serviceRecordService)
    {
        _serviceRecordService = serviceRecordService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var records = await _serviceRecordService.GetAllAsync();
        var sixMonthsAgo = DateTime.UtcNow.Date.AddMonths(-6);
        var overdueVehicles = records
         .GroupBy(x => new
         {
             x.VehicleId,
             x.VehicleLicensePlate
         })
         .Select(group => new OverdueMaintenanceViewModel
         {
             VehicleId = group.Key.VehicleId,
             VehicleLicensePlate = group.Key.VehicleLicensePlate,
             LastServiceDate = group.Max(x => x.ServiceDate)
         })
         .Where(x => x.LastServiceDate < sixMonthsAgo)
         .OrderBy(x => x.LastServiceDate)
         .ToList();

        return View(overdueVehicles);
    }
}