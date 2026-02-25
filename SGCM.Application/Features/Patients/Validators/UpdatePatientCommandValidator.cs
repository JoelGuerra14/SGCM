using FluentValidation;
using SGCM.Application.Features.Patients.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Validators
{
    public sealed class UpdatePatientCommandValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("El ID del paciente es requerido.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("El apellido es obligatorio.");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("El teléfono es obligatorio.");
            RuleFor(x => x.EmergencyContactName).NotEmpty().WithMessage("El contacto de emergencia es obligatorio.");
            RuleFor(x => x.EmergencyContactPhone).NotEmpty().WithMessage("El teléfono de emergencia es obligatorio.");

            RuleFor(x => x.PolicyNumber)
                .NotEmpty()
                .When(x => x.InsurerId.HasValue)
                .WithMessage("Debe ingresar el número de póliza si seleccionó una aseguradora.");
        }
    }
}
