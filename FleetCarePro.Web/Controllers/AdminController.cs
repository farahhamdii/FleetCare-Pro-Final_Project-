
using FleetCarePro.Domain.Entities;
using FleetCarePro.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FleetCarePro.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    // =========================
    // USERS
    // =========================

    [HttpGet]
    public async Task<IActionResult> Users()
    {
        var users = _userManager.Users.ToList();

        var userRoles = new Dictionary<string, string>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            userRoles[user.Id] =
                roles.FirstOrDefault() ?? "No Role";
        }

        ViewBag.UserRoles = userRoles;

        return View(users);
    }

    // =========================
    // CREATE USER - GET
    // =========================

    [HttpGet]
    public async Task<IActionResult> CreateUser()
    {
        await PopulateRolesAsync();

        return View();
    }

    // =========================
    // CREATE USER - POST
    // =========================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUser(
        CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateRolesAsync();
            return View(model);
        }

        if (!await _roleManager.RoleExistsAsync(model.Role))
        {
            ModelState.AddModelError(
                nameof(model.Role),
                "Selected role does not exist.");

            await PopulateRolesAsync();
            return View(model);
        }

        var existingEmail =
            await _userManager.FindByEmailAsync(model.Email);

        if (existingEmail != null)
        {
            ModelState.AddModelError(
                nameof(model.Email),
                "Email is already registered.");

            await PopulateRolesAsync();
            return View(model);
        }

        var existingEmployee =
            _userManager.Users.Any(
                u => u.EmployeeId == model.EmployeeId);

        if (existingEmployee)
        {
            ModelState.AddModelError(
                nameof(model.EmployeeId),
                "Employee ID is already registered.");

            await PopulateRolesAsync();
            return View(model);
        }

        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName,
            EmployeeId = model.EmployeeId,
            EmailConfirmed = true
        };

        var result =
            await _userManager.CreateAsync(
                user,
                model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            await PopulateRolesAsync();
            return View(model);
        }

        await _userManager.AddToRoleAsync(
            user,
            model.Role);

        TempData["SuccessMessage"] =
            "User created successfully.";

        return RedirectToAction(nameof(Users));
    }

    // =========================
    // EDIT USER - GET
    // =========================

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var user =
            await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        var roles =
            await _userManager.GetRolesAsync(user);

        var model = new EditUserViewModel
        {
            Id = user.Id,
            FullName = user.FullName,
            EmployeeId = user.EmployeeId,
            Email = user.Email ?? string.Empty,
            Role = roles.FirstOrDefault() ?? string.Empty
        };

        await PopulateRolesAsync();

        return View(model);
    }

    // =========================
    // EDIT USER - POST
    // =========================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditUser(
        EditUserViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateRolesAsync();
            return View(model);
        }

        if (!await _roleManager.RoleExistsAsync(model.Role))
        {
            ModelState.AddModelError(
                nameof(model.Role),
                "Selected role does not exist.");

            await PopulateRolesAsync();
            return View(model);
        }

        var user =
            await _userManager.FindByIdAsync(model.Id);

        if (user == null)
            return NotFound();

        var emailOwner =
            await _userManager.FindByEmailAsync(model.Email);

        if (emailOwner != null &&
            emailOwner.Id != user.Id)
        {
            ModelState.AddModelError(
                nameof(model.Email),
                "Email is already used by another user.");

            await PopulateRolesAsync();
            return View(model);
        }

        var employeeOwner =
            _userManager.Users.FirstOrDefault(
                u => u.EmployeeId == model.EmployeeId &&
                     u.Id != user.Id);

        if (employeeOwner != null)
        {
            ModelState.AddModelError(
                nameof(model.EmployeeId),
                "Employee ID is already used by another user.");

            await PopulateRolesAsync();
            return View(model);
        }

        user.FullName = model.FullName;
        user.EmployeeId = model.EmployeeId;
        user.Email = model.Email;
        user.UserName = model.Email;

        var updateResult =
            await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            await PopulateRolesAsync();
            return View(model);
        }

        var currentRoles =
            await _userManager.GetRolesAsync(user);

        if (currentRoles.Any())
        {
            await _userManager.RemoveFromRolesAsync(
                user,
                currentRoles);
        }

        await _userManager.AddToRoleAsync(
            user,
            model.Role);

        TempData["SuccessMessage"] =
            "User updated successfully.";

        return RedirectToAction(nameof(Users));
    }

    // =========================
    // DELETE USER - GET
    // =========================

    [HttpGet]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user =
            await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        return View(user);
    }

    // =========================
    // DELETE USER - POST
    // =========================

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteUserConfirmed(
        string id)
    {
        var user =
            await _userManager.FindByIdAsync(id);

        if (user == null)
            return NotFound();

        // Prevent admin from deleting their own account
        if (user.Id == _userManager.GetUserId(User))
        {
            TempData["ErrorMessage"] =
                "You cannot delete your own account.";

            return RedirectToAction(nameof(Users));
        }

        var result =
            await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(
                    string.Empty,
                    error.Description);
            }

            return View("DeleteUser", user);
        }

        TempData["SuccessMessage"] =
            "User deleted successfully.";

        return RedirectToAction(nameof(Users));
    }

    // =========================
    // ROLES DROPDOWN
    // =========================

    private async Task PopulateRolesAsync()
    {
        var roles =
            await _roleManager.Roles
                .Select(r => r.Name!)
                .ToListAsync();

        ViewBag.Roles = roles
            .Select(role => new SelectListItem
            {
                Value = role,
                Text = role
            })
            .ToList();
    }
}

