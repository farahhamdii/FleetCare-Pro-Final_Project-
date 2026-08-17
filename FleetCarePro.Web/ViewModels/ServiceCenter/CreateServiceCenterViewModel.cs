using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.ServiceCenter;

public class CreateServiceCenterViewModel
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Address { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public List<int> SelectedServiceCategoryIds { get; set; }
        = new List<int>();
}