using Microsoft.AspNetCore.Identity;

namespace SGCM.Shared.Domain.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    // Navigation properties - just one would've value
    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}