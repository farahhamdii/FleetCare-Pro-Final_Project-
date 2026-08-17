using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FleetCarePro.Web.ViewModels.Vehicle;

public class VehicleImageValidationAttribute : ValidationAttribute
{
    private const long MaxFileSize = 2 * 1024 * 1024;

    private static readonly string[] AllowedExtensions =
    {
        ".jpg",
        ".jpeg",
        ".png"
    };

    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        if (value is not IFormFile file)
            return new ValidationResult(
                "Invalid image file.");

        if (file.Length == 0)
            return new ValidationResult(
                "Image cannot be empty.");

        if (file.Length > MaxFileSize)
            return new ValidationResult(
                "Image size cannot exceed 2 MB.");

        var extension =
            Path.GetExtension(file.FileName)
                .ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
            return new ValidationResult(
                "Only JPG, JPEG and PNG images are allowed.");

        if (!file.ContentType.StartsWith("image/"))
            return new ValidationResult(
                "The uploaded file must be an image.");

        return ValidationResult.Success;
    }
}