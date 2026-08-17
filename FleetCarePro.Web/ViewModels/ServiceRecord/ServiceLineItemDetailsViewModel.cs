namespace FleetCarePro.Web.ViewModels.ServiceRecord;

public class ServiceLineItemDetailsViewModel
{
    public int ServiceCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string Description { get; set; } = null!;

    public decimal Cost { get; set; }
}