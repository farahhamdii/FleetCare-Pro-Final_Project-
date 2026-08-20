using AutoMapper;
using FleetCarePro.Application.DTOs.ServiceRecord;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Domain.Enums;
using FleetCarePro.Web.Filters;
using FleetCarePro.Web.ViewModels.ServiceRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FleetCarePro.Web.Controllers;

[Authorize(Roles = "Admin,FleetManager")]
public class ServiceRecordController : Controller
{
    private readonly IServiceRecordService _serviceRecordService;
    private readonly IVehicleService _vehicleService;
    private readonly IServiceCenterService _serviceCenterService;
    private readonly IServiceCategoryService _serviceCategoryService;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;

    public ServiceRecordController(
        IServiceRecordService serviceRecordService,
        IVehicleService vehicleService,
        IServiceCenterService serviceCenterService,
        IServiceCategoryService serviceCategoryService,
        IMapper mapper,
        IWebHostEnvironment environment)
    {
        _serviceRecordService = serviceRecordService;
        _vehicleService = vehicleService;
        _serviceCenterService = serviceCenterService;
        _serviceCategoryService = serviceCategoryService;
        _mapper = mapper;
        _environment = environment;
    }

    [HttpGet]

    public async Task<IActionResult> Index(string? status, int page = 1)
    {
        const int pageSize = 5;
        var records = await _serviceRecordService.GetAllAsync();

        if (!string.IsNullOrEmpty(status))
        {
            if (Enum.TryParse<ServiceRecordStatus>(status,true, out var parsedStatus))
            {
                records = records .Where(x => x.Status == parsedStatus).ToList();
            }
        }

        var totalRecords = records.Count();
        var totalPages = (int)Math.Ceiling(
            totalRecords / (double)pageSize);

        if (page < 1)
            page = 1;

        if (totalPages > 0 && page > totalPages)
            page = totalPages;

        var pagedRecords = records .Skip((page - 1) * pageSize).Take(pageSize).ToList();
        var viewModels = _mapper.Map<IEnumerable<ServiceRecordListViewModel>>( pagedRecords);

        ViewBag.StatusFilter = status;
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View(viewModels);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var record = await _serviceRecordService.GetByIdAsync(id);
        if (record == null)
            return NotFound();

        var viewModel =_mapper.Map<ServiceRecordDetailsViewModel>(record);
        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Filter(string? status)
    {
        var records = await _serviceRecordService.GetAllAsync();
        if (!string.IsNullOrEmpty(status) &&
            Enum.TryParse<ServiceRecordStatus>(status, true,out var parsedStatus))
        {
            records = records.Where(x => x.Status == parsedStatus).ToList();
        }
        var viewModels = _mapper.Map<IEnumerable<ServiceRecordListViewModel>>(records);
        return PartialView("_ServiceRecordsTable", viewModels);
    }


    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new CreateServiceRecordViewModel();
        await LoadDropdowns(model);
        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [AuditLog]
    public async Task<IActionResult> Create(CreateServiceRecordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);
            return View(model);
        }

        string? invoicePath = null;

        if (model.InvoiceFile != null &&
            model.InvoiceFile.Length > 0)
        {
            var extension = Path.GetExtension(model.InvoiceFile.FileName)
                    .ToLowerInvariant();

            var allowedExtensions = new[]
            {
                ".pdf",
                ".jpg",
                ".jpeg",
                ".png"
            };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError( "InvoiceFile","Only PDF, JPG, JPEG and PNG files are allowed.");
                await LoadDropdowns(model);
                return View(model);
            }

            if (model.InvoiceFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("InvoiceFile", "Invoice file size must not exceed 5 MB.");
                await LoadDropdowns(model);

                return View(model);
            }

            var uploadsFolder =Path.Combine( _environment.WebRootPath,"uploads","invoices");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName =$"{Guid.NewGuid()}{extension}";

            var filePath =Path.Combine(uploadsFolder,
                    uniqueFileName);

            using (var stream =
                   new FileStream(
                       filePath,
                       FileMode.Create))
            {
                await model.InvoiceFile.CopyToAsync(stream);
            }

            invoicePath =  $"/uploads/invoices/{uniqueFileName}";
        }


        var dto =  _mapper.Map<CreateServiceRecordDto>(model);

        dto.CreatedByUserId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier);


        dto.InvoiceDocumentPath = invoicePath;
        await _serviceRecordService.CreateAsync(dto);
        TempData["SuccessMessage"] = "Service record created successfully.";

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var record =await _serviceRecordService.GetByIdAsync(id);
        if (record == null)
            return NotFound();
        var model =_mapper.Map<EditServiceRecordViewModel>( record);
        await LoadDropdowns(model);
        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [AuditLog]
    public async Task<IActionResult> Edit(
        EditServiceRecordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns(model);

            return View(model);
        }

        var existingRecord = await _serviceRecordService.GetByIdAsync(model.Id);

        if (existingRecord == null)
            return NotFound();

        string? invoicePath = existingRecord.InvoiceDocumentPath;


        if (model.InvoiceFile != null && model.InvoiceFile.Length > 0)
        {
            var extension = Path.GetExtension(model.InvoiceFile.FileName) .ToLowerInvariant();
            var allowedExtensions = new[]
            {
                ".pdf",
                ".jpg",
                ".jpeg",
                ".png"
            };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(
                    "InvoiceFile",
                    "Only PDF, JPG, JPEG and PNG files are allowed.");

                await LoadDropdowns(model);

                return View(model);
            }

            if (model.InvoiceFile.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError(
                    "InvoiceFile",
                    "Invoice file size must not exceed 5 MB.");

                await LoadDropdowns(model);

                return View(model);
            }

            var uploadsFolder =
                Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "invoices");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName =
                $"{Guid.NewGuid()}{extension}";

            var filePath =
                Path.Combine(
                    uploadsFolder,
                    uniqueFileName);

            using (var stream =
                   new FileStream(
                       filePath,
                       FileMode.Create))
            {
                await model.InvoiceFile.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(
                    existingRecord.InvoiceDocumentPath))
            {
                var oldFilePath =
                    Path.Combine(
                        _environment.WebRootPath,
                        existingRecord.InvoiceDocumentPath
                            .TrimStart('/')
                            .Replace(
                                '/',
                                Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            invoicePath =
                $"/uploads/invoices/{uniqueFileName}";
        }

        var dto =  _mapper.Map<UpdateServiceRecordDto>(model);
        dto.InvoiceDocumentPath = invoicePath;
        var success = await _serviceRecordService.UpdateAsync( model.Id,  dto);

        if (!success)
            return NotFound();

        TempData["SuccessMessage"] = "Service record updated successfully.";

        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [AuditLog]
    public async Task<IActionResult> Delete(int id)
    {
        var record = await _serviceRecordService.GetByIdAsync(id);

        if (record == null)
            return NotFound();

        if (!string.IsNullOrEmpty(
                record.InvoiceDocumentPath))
        {
            var filePath =
                Path.Combine(
                    _environment.WebRootPath,
                    record.InvoiceDocumentPath
                        .TrimStart('/')
                        .Replace(
                            '/',
                            Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        var success = await _serviceRecordService.DeleteAsync(id);
        if (!success)return NotFound();
        TempData["SuccessMessage"] = "Service record deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdowns(
        CreateServiceRecordViewModel model)
    {
        var vehicles = await _vehicleService.GetAllAsync();
        var serviceCenters = await _serviceCenterService.GetAllAsync();
        var categories = await _serviceCategoryService.GetAllAsync();

        ViewBag.Vehicles =
            new SelectList(
                vehicles,
                "Id",
                "LicensePlate",
                model.VehicleId);

        ViewBag.ServiceCenters =
            new SelectList(
                serviceCenters,
                "Id",
                "Name",
                model.ServiceCenterId);

        ViewBag.ServiceCategories =
            new SelectList(
                categories,
                "Id",
                "CategoryName");
    }


    private async Task LoadDropdowns(
        EditServiceRecordViewModel model)
    {
        var vehicles =await _vehicleService.GetAllAsync();
        var serviceCenters =await _serviceCenterService.GetAllAsync();
        var categories = await _serviceCategoryService.GetAllAsync();

        ViewBag.Vehicles =
            new SelectList(
                vehicles,
                "Id",
                "LicensePlate",
                model.VehicleId);

        ViewBag.ServiceCenters =
            new SelectList(
                serviceCenters,
                "Id",
                "Name",
                model.ServiceCenterId);

        ViewBag.ServiceCategories =
            new SelectList(
                categories,
                "Id",
                "CategoryName");
    }
}