namespace FleetCarePro.Application.DTOs.ServiceRecord;

public class ServiceLineItemDto
{
    public int Id { get; set; }

    public int ServiceCategoryId { get; set; }

    public string? CategoryName { get; set; }

    public string Description { get; set; } = null!;

    public decimal Cost { get; set; }
}