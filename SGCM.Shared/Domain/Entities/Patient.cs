using SGCM.Shared.Domain.ValueObjects;

namespace SGCM.Shared.Domain.Entities;

public sealed class Patient
{
    public PatientId Id { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public string? BloodType { get; private set; }
    public string? Allergies { get; private set; }
    public string? MedicalConditions { get; private set; }

    // Emergency Contact
    public string? EmergencyContactName { get; private set; }
    public string? EmergencyContactPhone { get; private set; }

    // Insurer Info
    public InsurerId? InsurerId { get; private set; }
    public string? PolicyNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    public ApplicationUser User { get; private set; } = null!;
    public Insurer? Insurer { get; private set; }

    private Patient() { } // Empty ctor for EF Core

    public static Patient Create(string userId, string firstName, string lastName)
    {
        return new Patient
        {
            Id = PatientId.New(),
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateProfile(string phone, string? address, string? bloodType,
        string? allergies, string? medicalConditions, string emergencyContactName,
        string emergencyContactPhone, InsurerId? insurerId, string? policyNumber)
    {
        Phone = phone;
        Address = address;
        BloodType = bloodType;
        Allergies = allergies;
        MedicalConditions = medicalConditions;
        EmergencyContactName = emergencyContactName;
        EmergencyContactPhone = emergencyContactPhone;
        InsurerId = insurerId;
        PolicyNumber = policyNumber;
    }
}