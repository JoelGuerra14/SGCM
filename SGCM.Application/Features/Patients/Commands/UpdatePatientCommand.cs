using SGCM.Application.Contracts;
using SGCM.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Commands
{
    public sealed record UpdatePatientCommand(
        Guid Id,
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
    ) : ICommand<Result>;
}
