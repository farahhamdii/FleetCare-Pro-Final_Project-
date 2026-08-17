namespace FleetCarePro.Web.ViewModels.ServiceCategory;

public class ServiceCategoryListViewModel
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public int RecommendedIntervalMonths { get; set; }
}