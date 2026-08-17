using FleetCarePro.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetCarePro.Web.Components;

public class FleetCostSummaryViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public FleetCostSummaryViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var startOfMonth = new DateTime(
            DateTime.Today.Year,
            DateTime.Today.Month,
            1);

        var startOfNextMonth = startOfMonth.AddMonths(1);

        var totalCost = await _context.ServiceRecords
            .Where(sr =>
                sr.ServiceDate >= startOfMonth &&
                sr.ServiceDate < startOfNextMonth)
            .SumAsync(sr => (decimal?)sr.TotalCost) ?? 0;

        return View(totalCost);
    }
}