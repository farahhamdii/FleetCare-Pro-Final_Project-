using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Domain.Enums;
using FleetCarePro.Web.ViewModels.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FleetCarePro.Web.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly IVehicleService _vehicleService;
    private readonly IServiceCenterService _serviceCenterService;
    private readonly IServiceRecordService _serviceRecordService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IServiceCategoryService _serviceCategoryService;

    public DashboardController(
        IVehicleService vehicleService,
        IServiceCenterService serviceCenterService,
        IServiceRecordService serviceRecordService,
        IServiceCategoryService serviceCategoryService,
        UserManager<ApplicationUser> userManager)
    {
        _vehicleService = vehicleService;
        _serviceCenterService = serviceCenterService;
        _serviceRecordService = serviceRecordService;
        _serviceCategoryService = serviceCategoryService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            WelcomeMessage = $"Welcome, {User.Identity?.Name}"
        };

        // =========================
        // ADMIN
        // =========================

        if (User.IsInRole("Admin"))
        {
            var admins =
                await _userManager.GetUsersInRoleAsync("Admin");

            var managers =
                await _userManager.GetUsersInRoleAsync("FleetManager");

            var drivers =
                await _userManager.GetUsersInRoleAsync("Driver");

            model.TotalUsers =
                admins.Count +
                managers.Count +
                drivers.Count;

            var vehicles =
                await _vehicleService.GetAllAsync();

            var centers =
                await _serviceCenterService.GetAllAsync();

            var records =
                await _serviceRecordService.GetAllAsync();

            var categories =
                await _serviceCategoryService.GetAllAsync();

            model.TotalVehicles =
                vehicles.Count();

            model.TotalServiceCenters =
                centers.Count();

            model.TotalServiceRecords =
                records.Count();

            model.TotalServiceCategories =
                categories.Count();

            model.PendingServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Pending);

            model.ApprovedServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Approved);

            model.CompletedServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Completed);

            model.CancelledServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Cancelled);
        }

        // =========================
        // FLEET MANAGER
        // =========================

        else if (User.IsInRole("FleetManager"))
        {
            var vehicles =
                await _vehicleService.GetAllAsync();

            var centers =
                await _serviceCenterService.GetAllAsync();

            var records =
                await _serviceRecordService.GetAllAsync();

            model.TotalVehicles =
                vehicles.Count();

            model.TotalServiceCenters =
                centers.Count();

            model.TotalServiceRecords =
                records.Count();

            // =========================
            // SERVICE STATUS COUNTS
            // =========================

            model.PendingServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Pending);

            model.ApprovedServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Approved);

            model.CompletedServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Completed);

            model.CancelledServices =
                records.Count(x =>
                    x.Status == ServiceRecordStatus.Cancelled);
        }

        // =========================
        // DRIVER
        // =========================

        else if (User.IsInRole("Driver"))
        {
            var driverId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(driverId))
                return Forbid();

            var vehicles =
                await _vehicleService
                    .GetAssignedVehiclesAsync(driverId);

            model.AssignedVehicles =
                vehicles.Count();

            model.TotalVehicles =
                vehicles.Count();
        }

        return View(model);
    }
}