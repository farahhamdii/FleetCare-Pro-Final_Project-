using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FleetCarePro.Web.Validation;

public class ValidVINAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object? value,
        ValidationContext validationContext)
    {
        if (value is not string vin || string.IsNullOrWhiteSpace(vin))
            return new ValidationResult("VIN is required.");

        if (vin.Length != 17)
            return new ValidationResult("VIN must be exactly 17 characters.");

        if (!Regex.IsMatch(vin, @"^[A-HJ-NPR-Z0-9]{17}$"))
            return new ValidationResult(
                "VIN must contain only valid letters and numbers.");

        return ValidationResult.Success;
    }
}