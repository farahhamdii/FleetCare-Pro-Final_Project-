namespace FleetCarePro.Application.DTOs.ServiceCenter;

public class UpdateServiceCenterDto
{
    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public bool IsActive { get; set; }

    public List<int> SelectedServiceCategoryIds { get; set; }
        = new List<int>();
}