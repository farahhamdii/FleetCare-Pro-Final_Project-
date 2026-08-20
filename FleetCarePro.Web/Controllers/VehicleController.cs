using AutoMapper;
using FleetCarePro.Application.DTOs.Vehicle;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Entities;
using FleetCarePro.Web.Filters;
using FleetCarePro.Web.ViewModels.Vehicle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FleetCarePro.Web.Controllers;

[Authorize]
public class VehicleController : Controller
{
    private readonly IVehicleService _vehicleService;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public VehicleController(
        IVehicleService vehicleService,
        IFileService fileService,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _vehicleService = vehicleService;
        _fileService = fileService;
        _mapper = mapper;
        _userManager = userManager;
    }

    private async Task PopulateDriversDropDownListAsync()
    {
        var drivers = await _userManager.GetUsersInRoleAsync("Driver");

        ViewBag.Drivers = drivers.Select(d => new SelectListItem
        {
            Value = d.Id,
            Text = $"{d.FullName} ({d.EmployeeId})"
        });
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IEnumerable<VehicleDto> vehicles;

        if (User.IsInRole("Driver"))
        {
            var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(driverId))
                return Forbid();
            vehicles = await _vehicleService.GetAssignedVehiclesAsync(driverId);
        }
        else
        {
            vehicles = await _vehicleService.GetAllAsync();
        }

        var viewModels = _mapper.Map<IEnumerable<VehicleListViewModel>>(vehicles);

        return View(viewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        VehicleDto? vehicle;
        if (User.IsInRole("Driver"))
        {
            var driverId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(driverId))
                return Forbid();

            vehicle = await _vehicleService.GetByIdForDriverAsync(id, driverId);
        }
        else
        {
            vehicle = await _vehicleService.GetByIdAsync(id);
        }

        if (vehicle == null)
            return NotFound();

        var viewModel = _mapper.Map<VehicleDetailsViewModel>(vehicle);

        return View(viewModel);
    }

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateDriversDropDownListAsync();

        return View();
    }

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateVehicleViewModel vehicle)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDriversDropDownListAsync();
            return View(vehicle);
        }

        string? imageUrl = null;

        if (vehicle.VehicleImage != null)
        {
            await using var stream = vehicle.VehicleImage.OpenReadStream();
            imageUrl = await _fileService.UploadAsync( stream,vehicle.VehicleImage.FileName, "vehicles");
        }

        var dto = _mapper.Map<CreateVehicleDto>(vehicle);

        dto.VehicleImageURL = imageUrl;

        await _vehicleService.CreateAsync(dto);

        TempData["SuccessMessage"] = "Vehicle created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);

        if (vehicle == null)
            return NotFound();

        var model = _mapper.Map<EditVehicleViewModel>(vehicle);
        model.ExistingImageUrl = vehicle.VehicleImageURL;
        await PopulateDriversDropDownListAsync();
        return View(model);
    }

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditVehicleViewModel vehicle)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDriversDropDownListAsync();
            return View(vehicle);
        }

        string? imageUrl = vehicle.ExistingImageUrl;

        if (vehicle.VehicleImage != null)
        {
            if (!string.IsNullOrEmpty(vehicle.ExistingImageUrl))
            {
                await _fileService.DeleteAsync(
                    vehicle.ExistingImageUrl);
            }

            await using var stream =vehicle.VehicleImage.OpenReadStream();

            imageUrl = await _fileService.UploadAsync(stream, vehicle.VehicleImage.FileName, "vehicles");
        }

        var dto = _mapper.Map<UpdateVehicleDto>(vehicle);
        dto.VehicleImageURL = imageUrl;
        var success =await _vehicleService.UpdateAsync(vehicle.Id, dto);

        if (!success)return NotFound();

        TempData["SuccessMessage"] = "Vehicle updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var vehicle = await _vehicleService.GetByIdAsync(id);
        if (vehicle == null)
            return NotFound();
        var success = await _vehicleService.DeleteAsync(id);
        if (!success)
        {
            TempData["ErrorMessage"] ="This vehicle cannot be deleted because it has service records.";

            return RedirectToAction(nameof(Index));
        }
        if (!string.IsNullOrEmpty(vehicle.VehicleImageURL))
        {
            await _fileService.DeleteAsync(vehicle.VehicleImageURL);
        }
        TempData["SuccessMessage"] = "Vehicle deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}