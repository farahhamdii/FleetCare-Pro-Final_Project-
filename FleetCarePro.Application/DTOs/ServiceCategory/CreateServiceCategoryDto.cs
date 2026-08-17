namespace FleetCarePro.Application.DTOs.ServiceCategory;

public class CreateServiceCategoryDto
{
    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public int RecommendedIntervalMonths { get; set; }
}