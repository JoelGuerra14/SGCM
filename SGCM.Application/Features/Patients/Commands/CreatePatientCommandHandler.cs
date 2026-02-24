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
    public sealed class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, Result>
    {
        private readonly AppDbContext _dbContext;
        private readonly IValidator<CreatePatientCommand> _validator;

        public CreatePatientCommandHandler(AppDbContext dbContext, IValidator<CreatePatientCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result> Handle(CreatePatientCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                var error = validationResult.Errors.First();
                return Result.Failure(Error.Validation("ValidationFailed", error.ErrorMessage));
            }

            var patient = await _dbContext.Patients
                .FirstOrDefaultAsync(p => p.UserId == command.UserId, cancellationToken);

            if (patient is null)
                return Result.Failure(Error.NotFound("Patient.NotFound", "No se encontró el paciente asociado a este usuario."));

            InsurerId? insurerId = command.InsurerId.HasValue ? InsurerId.From(command.InsurerId.Value) : null;

            patient.UpdateProfile(
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

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
