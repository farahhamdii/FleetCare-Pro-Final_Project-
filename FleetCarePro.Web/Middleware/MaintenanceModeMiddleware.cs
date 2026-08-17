using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace FleetCarePro.Web.Middleware;

public class MaintenanceModeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public MaintenanceModeMiddleware(RequestDelegate next,IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var isMaintenanceMode =_configuration.GetValue<bool>("MaintenanceMode:IsEnabled");

        if (isMaintenanceMode &&
            !context.Request.Path.StartsWithSegments("/Home"))
        {
            context.Response.Redirect("/Home/Maintenance");
            return;
        }

        await _next(context);
    }
}