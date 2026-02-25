using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SGCM.Application.Contracts;
using SGCM.Infrastructure.DbContexts;
using SGCM.Shared.Domain.ValueObjects;
using SGCM.Shared.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Commands
{
    public sealed class UpdatePatientCommandHandler : ICommandHandler<UpdatePatientCommand, Result>
    {
        private readonly AppDbContext _dbContext;
        private readonly IValidator<UpdatePatientCommand> _validator;

        public UpdatePatientCommandHandler(AppDbContext dbContext, IValidator<UpdatePatientCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result> Handle(UpdatePatientCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First();
                return Result.Failure(Error.Validation("ValidationFailed", error.ErrorMessage));
            }

            var patientId = PatientId.From(command.Id);
            var patient = await _dbContext.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == patientId, cancellationToken);

            if (patient is null)
                return Result.Failure(Error.NotFound("Patient.NotFound", "El paciente no existe."));

            InsurerId? insurerId = command.InsurerId.HasValue ? InsurerId.From(command.InsurerId.Value) : null;

            patient.UpdateProfile(
                command.FirstName,
                command.LastName,
                command.Phone,
                command.Address,
                command.BloodType,
                command.Allergies,
                command.MedicalConditions,
                command.EmergencyContactName,
                command.EmergencyContactPhone,
                insurerId,
                command.PolicyNumber
            );

            patient.User.FirstName = command.FirstName;
            patient.User.LastName = command.LastName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
