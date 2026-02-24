using SGCM.Shared.Domain.ValueObjects;

namespace SGCM.Shared.Domain.Entities;

public sealed class Doctor
{
    public DoctorId Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;

    // Navigation properties
    public ApplicationUser User { get; private set; } = null!;

    private Doctor() { } // Empty ctor for EF Core

    public static Doctor Create(string userId, string firstName, string lastName)
    {
        return new Doctor
        {
            Id = DoctorId.New(),
            UserId = userId,
            FirstName = firstName,
            LastName = lastName
        };
    }
}