using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceCenter;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Web.Filters;
using FleetCarePro.Web.ViewModels.ServiceCenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FleetCarePro.Web.Controllers;

[Authorize(Roles = "Admin,FleetManager")]
public class ServiceCenterController : Controller
{
    private readonly IServiceCenterService _serviceCenterService;
    private readonly IServiceCategoryService _serviceCategoryService;
    private readonly IMapper _mapper;

    public ServiceCenterController( IServiceCenterService serviceCenterService,
        IServiceCategoryService serviceCategoryService,
        IMapper mapper)
    {
        _serviceCenterService = serviceCenterService;
        _serviceCategoryService = serviceCategoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var serviceCenters =await _serviceCenterService.GetAllAsync();
        var viewModels = _mapper.Map<IEnumerable<ServiceCenterListViewModel>>(serviceCenters);
        return View(viewModels);
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var serviceCenter = await _serviceCenterService.GetByIdAsync(id);
        if (serviceCenter == null)
            return NotFound();
        var viewModel = _mapper.Map<ServiceCenterDetailsViewModel>( serviceCenter);
        return View(viewModel);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model =new CreateServiceCenterViewModel();
        await LoadServiceCategories(model);
        return View(model);
    }


    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateServiceCenterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadServiceCategories(model);
            return View(model);
        }

        var dto = _mapper.Map<CreateServiceCenterDto>(model);
        await _serviceCenterService.CreateAsync(dto);
        TempData["SuccessMessage"] ="Service center created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var serviceCenter = await _serviceCenterService.GetByIdAsync(id);

        if (serviceCenter == null)
            return NotFound();

        var model = _mapper.Map<EditServiceCenterViewModel>(serviceCenter);
        await LoadServiceCategories(model);
        return View(model);
    }


    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditServiceCenterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadServiceCategories(model);
            return View(model);
        }

        var dto = _mapper.Map<UpdateServiceCenterDto>(model);
        var success =await _serviceCenterService.UpdateAsync( model.Id, dto);

        if (!success)
            return NotFound();
        TempData["SuccessMessage"] ="Service center updated successfully.";
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [AuditLog]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var success =await _serviceCenterService.DeleteAsync(id);
        if (!success) return NotFound();

        TempData["SuccessMessage"] = "Service center deleted successfully.";
        return RedirectToAction(nameof(Index));
    }


    private async Task LoadServiceCategories(CreateServiceCenterViewModel model)
    {
        var categories = await _serviceCategoryService.GetAllAsync();

        ViewBag.ServiceCategories =categories.Select(category =>
                new SelectListItem
                {
                    Value = category.Id.ToString(),
                    Text = category.CategoryName,
                    Selected =model.SelectedServiceCategoryIds.Contains(category.Id)
                }).ToList();
    }
    private async Task LoadServiceCategories( EditServiceCenterViewModel model)
    {
        var categories =await _serviceCategoryService.GetAllAsync();

        ViewBag.ServiceCategories =categories.Select(category =>
                new SelectListItem
                {
                    Value=category.Id.ToString(),
                    Text=category.CategoryName,
                    Selected=model.SelectedServiceCategoryIds .Contains(category.Id)
                }).ToList();
    }
}