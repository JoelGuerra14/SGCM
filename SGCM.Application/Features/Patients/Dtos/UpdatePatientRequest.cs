using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Dtos
{
    public record UpdatePatientRequest(
        string FirstName,
        string LastName,
        string Phone,
        string? Address,
        string? BloodType,
        string? Allergies,
        string? MedicalConditions,
        string EmergencyContactName,
        string EmergencyContactPhone,
        Guid? InsurerId,
        string? PolicyNumber
    );
}
