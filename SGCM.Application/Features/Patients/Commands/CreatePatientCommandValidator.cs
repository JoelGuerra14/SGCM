using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCM.Application.Features.Patients.Commands
{
    internal class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
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
