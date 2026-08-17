namespace FleetCarePro.Application.DTOs.ServiceCategory;

public class UpdateServiceCategoryDto
{
    public string CategoryName { get; set; } = null!;
    public string? Description { get; set; }
    public int RecommendedIntervalMonths { get; set; }
}