using FleetCarePro.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FleetCarePro.Web.Filters;

public class AuditLogAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        // Execute the action first
        var executedContext = await next();

        // Only audit Create / Edit / Update / Delete actions
        var actionName =
            context.ActionDescriptor.RouteValues["action"];

        if (actionName is not ("Create" or "Edit" or "Update" or "Delete"))
            return;

        // Get AuditLogService from DI
        var auditLogService =
            context.HttpContext.RequestServices
                .GetRequiredService<IAuditLogService>();

        // Get current user ID
        var userId =
            context.HttpContext.User.FindFirstValue(
                ClaimTypes.NameIdentifier);

        // Controller name
        var controllerName =
            context.ActionDescriptor.RouteValues["controller"];

        var details =
            $"{controllerName}.{actionName} executed";

        await auditLogService.LogAsync(
            actionName,
            details,
            userId);
    }
}