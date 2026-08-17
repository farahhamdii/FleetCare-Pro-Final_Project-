
using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.Admin;

public class CreateUserViewModel
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string EmployeeId { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!;
}

