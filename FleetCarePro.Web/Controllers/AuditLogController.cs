
using AutoMapper;
using FleetCarePro.Application.Interfaces.Services;
using FleetCarePro.Web.ViewModels.AuditLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetCarePro.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AuditLogController : Controller
{
    private readonly IAuditLogService _auditLogService;
    private readonly IMapper _mapper;

    public AuditLogController( IAuditLogService auditLogService,IMapper mapper)
    {
        _auditLogService = auditLogService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var logs = await _auditLogService.GetAllAsync();
        var viewModels = _mapper.Map<IEnumerable<AuditLogViewModel>>(logs);
        return View(viewModels);
    }
}
