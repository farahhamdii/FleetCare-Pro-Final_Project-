
using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.Admin;

public class EditUserViewModel
{
    public string Id { get; set; } = null!;

    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string EmployeeId { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!;
}