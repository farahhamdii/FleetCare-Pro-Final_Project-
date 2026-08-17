using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceCategory;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Web.Filters;
using FleetCarePro.Web.ViewModels.ServiceCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetCarePro.Web.Controllers;

[Authorize(Roles = "Admin,FleetManager")]
public class ServiceCategoryController : Controller
{
    private readonly IServiceCategoryService _serviceCategoryService;
    private readonly IMapper _mapper;

    public ServiceCategoryController(
        IServiceCategoryService serviceCategoryService,
        IMapper mapper)
    {
        _serviceCategoryService = serviceCategoryService;
        _mapper = mapper;
    }

    // =========================
    // INDEX
    // =========================

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories =
            await _serviceCategoryService.GetAllAsync();

        var viewModels =
            _mapper.Map<IEnumerable<ServiceCategoryListViewModel>>(
                categories);

        return View(viewModels);
    }

    // =========================
    // DETAILS
    // =========================

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var category =
            await _serviceCategoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        var viewModel =
            _mapper.Map<ServiceCategoryDetailsViewModel>(
                category);

        return View(viewModel);
    }

    // =========================
    // CREATE - GET
    // =========================

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // =========================
    // CREATE - POST
    // =========================

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateServiceCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto =
            _mapper.Map<CreateServiceCategoryDto>(model);

        await _serviceCategoryService.CreateAsync(dto);

        TempData["SuccessMessage"] =
            "Service category created successfully.";

        return RedirectToAction(nameof(Index));
    }

    // =========================
    // EDIT - GET
    // =========================

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var category =
            await _serviceCategoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound();

        var model =
            _mapper.Map<EditServiceCategoryViewModel>(
                category);

        return View(model);
    }

    // =========================
    // EDIT - POST
    // =========================

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        EditServiceCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var dto =
            _mapper.Map<UpdateServiceCategoryDto>(model);

        var success =
            await _serviceCategoryService.UpdateAsync(
                model.Id,
                dto);

        if (!success)
            return NotFound();

        TempData["SuccessMessage"] =
            "Service category updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    // =========================
    // DELETE
    // =========================

    [Authorize(Roles = "Admin,FleetManager")]
    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var success =
            await _serviceCategoryService.DeleteAsync(id);

        if (!success)
            return NotFound();

        TempData["SuccessMessage"] =
            "Service category deleted successfully.";

        return RedirectToAction(nameof(Index));
    }
}