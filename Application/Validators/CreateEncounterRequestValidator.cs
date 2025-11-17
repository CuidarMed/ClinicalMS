using Application.DTOs;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class CreateEncounterRequestValidator: AbstractValidator<CreateEncounterRequest>
    {
        public CreateEncounterRequestValidator() {
            RuleFor(x => x.PatientId)
                .GreaterThan(0)
                .WithMessage("El ID del paciente es obligatorio");

            RuleFor(x => x.DoctorId)
                .GreaterThan(0)
                .WithMessage("El ID del doctor es obligatorio");

            RuleFor(x => x.AppointmentId)
                .GreaterThan(0)
                .WithMessage("El ID de la cita es obligatorio");

            RuleFor(x => x.Reasons)
                .NotEmpty()
                .WithMessage("Los motivos de consulta son obligatorios")
                .MaximumLength(1000)
                .WithMessage("Los motivos no pueden exeder 1000 caracteres");

            RuleFor(x => x.Subjective)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Subjective))
                .WithMessage("El campo subjetivo no debe execeder los 2000 caracteres");

            RuleFor(x => x.Objetive)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Objetive))
                .WithMessage("El campo objetivo no puede exceder 2000 caracteres");

            RuleFor(x => x.Assessment)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Assessment))
                .WithMessage("La evaluacion no  puede exceder 2000 caracteres");

            RuleFor(x => x.Plan)
                .MaximumLength(2000)
                .When(x => !string.IsNullOrEmpty(x.Plan))
                .WithMessage("El plan no puede exceder 2000 caracteres");

            RuleFor(x => x.Notes)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrEmpty(x.Notes))
                .WithMessage("La nota no debe superar los 1000 caracteres");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("El estado es obligatorio")
                .Must(status => status == "Open" || status == "Signed")
                .WithMessage("El estado debe ser Open o Signed");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("La fecha es obligatoria")
                .LessThanOrEqualTo(DateTime.Now.AddDays(1))
                .WithMessage("La fecha no puede ser futura");
        }
    }
}
