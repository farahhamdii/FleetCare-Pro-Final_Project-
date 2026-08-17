using FleetCarePro.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FleetCarePro.Web.Data;

public static class UserSeeder
{
    public static async Task SeedUsersAsync(
        UserManager<ApplicationUser> userManager)
    {
        await CreateUserAsync(
            userManager,
            "admin@fleetcarepro.com",
            "Admin@123",
            "System Admin",
            "EMP001",
            "Admin");

        await CreateUserAsync(
            userManager,
            "manager@fleetcarepro.com",
            "Manager@123",
            "Fleet Manager",
            "EMP002",
            "FleetManager");

        await CreateUserAsync(
            userManager,
            "driver@fleetcarepro.com",
            "Driver@123",
            "Fleet Driver",
            "EMP003",
            "Driver");
    }

    private static async Task CreateUserAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string password,
        string fullName,
        string employeeId,
        string role)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user != null)
            return;

        user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            EmployeeId = employeeId,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join( ", ", result.Errors.Select(e => e.Description));
            throw new Exception(  $"Failed to create {role} user: {errors}");
        }

        await userManager.AddToRoleAsync(user, role);
    }
}