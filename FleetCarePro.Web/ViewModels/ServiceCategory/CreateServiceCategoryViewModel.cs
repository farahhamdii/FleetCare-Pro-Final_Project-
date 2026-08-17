using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.ServiceCategory;

public class CreateServiceCategoryViewModel
{
    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; } = null!;

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(1, 120)]
    public int RecommendedIntervalMonths { get; set; }
}