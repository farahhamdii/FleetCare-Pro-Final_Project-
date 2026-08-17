using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.ServiceRecord;

public class ServiceLineItemViewModel
{
    public int Id { get; set; }

    [Required]
    public int ServiceCategoryId { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(0, 100000000)]
    public decimal Cost { get; set; }
}