using Microsoft.EntityFrameworkCore;
using SGCM.Application.Contracts;
using SGCM.Application.Features.Patients.Dtos;
using SGCM.Infrastructure.DbContexts;
using SGCM.Shared.Domain.ValueObjects;
using SGCM.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Queries
{
    public sealed class GetPatientByIdQueryHandler : IQueryHandler<GetPatientByIdQuery, Result<PatientResponseDto>>
    {
        private readonly AppDbContext _dbContext;

        public GetPatientByIdQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PatientResponseDto>> Handle(GetPatientByIdQuery query, CancellationToken cancellationToken = default)
        {
            var patientId = PatientId.From(query.Id);

            var patient = await _dbContext.Patients
                .AsNoTracking() 
                .Include(p => p.User)
                .Include(p => p.Insurer)
                .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);

            if (patient is null)
                return Result<PatientResponseDto>.Failure(Error.NotFound("Patient.NotFound", "El paciente no existe."));

            var dto = new PatientResponseDto(
                patient.Id.Value,
                patient.FirstName,
                patient.LastName,
                patient.User.Email, 
                patient.Phone,
                patient.Address,
                patient.BloodType,
                patient.Allergies,
                patient.MedicalConditions,
                patient.EmergencyContactName,
                patient.EmergencyContactPhone,
                patient.PolicyNumber,
                patient.Insurer != null ? new InsurerDto(patient.Insurer.Id.Value, patient.Insurer.Name) : null
            );

            return Result<PatientResponseDto>.Success(dto);
        }
    }
}
