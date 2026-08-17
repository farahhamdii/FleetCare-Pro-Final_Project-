namespace FleetCarePro.Application.DTOs.ServiceCategory;

public class ServiceCategoryDto
{
    public int Id { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public int RecommendedIntervalMonths { get; set; }
}