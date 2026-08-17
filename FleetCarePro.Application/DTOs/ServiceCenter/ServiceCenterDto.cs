namespace FleetCarePro.Application.DTOs.ServiceCenter;

public class ServiceCenterDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsActive { get; set; }

    public List<int> SelectedServiceCategoryIds { get; set; }
        = new List<int>();

    public List<string> ServiceCategories { get; set; }
        = new List<string>();
}