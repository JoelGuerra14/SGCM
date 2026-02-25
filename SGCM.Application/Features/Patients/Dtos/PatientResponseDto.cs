using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Dtos
{
    public record PatientResponseDto(
        Guid Id,
        string FirstName,
        string LastName,
        string? Email,
        string? Phone,
        string? Address,
        string? BloodType,
        string? Allergies,
        string? MedicalConditions,
        string? EmergencyContactName,
        string? EmergencyContactPhone,
        string? PolicyNumber,
        InsurerDto? Insurer 
    );

    public record InsurerDto(
        Guid Id,
        string Name
    );
}
